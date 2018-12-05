using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Player : MovingObject
{
    public struct PlayerStats
    {
        public int lives;
        public int kills;
        public int damageDealt;
        public int damageTaken;
    }
    private PlayerStats myStats = new PlayerStats();

    private InputManager _myInputManager;
    private int _healthRemaining;
    private float _manaRemaining;
    private int _livesRemaining;
    private bool _isDead = false;
    private float _deathTime = 0;
    private bool _invulnerable = false;
    private float _invulnerableStartTime = 0;
    private SpellDatabase.Element _firstElement = SpellDatabase.Element.Fire;
    private SpellDatabase.Element _secondElement = SpellDatabase.Element.Water;

    private SpriteRenderer _myIndicator;
    private Transform _myCirclePointer;
    private bool _disableMovement = false;
    private bool _castingSpell = false;
    private ParticleSystem _castingParticle;
    private ParticleSystem _jumpParticle;
    private bool _isSwitchingElement = false;
    private bool _hasSwitchedElement = false;
    private float _changedElementTime;
    private const float _changedElementDelay = 1;
    private float nextActionTime = 0;

    private bool _grounded = false;
    private bool _usedDoubleJump = false;
    private float _jumpTime = 0;
    private int ID;

    private float steamCloudDamage = 0;
    public bool IsInSteamCloud = false; //Fix mana cost, fix pillar.
    EventInstance soundPlayerEvent;
    EventInstance soundSpellsEvent;
    EventInstance soundSpellsEventFire;


    public Player()
    {

    }

    public Player GetPlayer(int pPlayerIndex)
    {
        ID = pPlayerIndex;
        Debug.Log("Added player with ID: " + pPlayerIndex);
        _healthRemaining = Glob.maxHealth;
        _manaRemaining = Glob.maxMana;
        _livesRemaining = Glob.maxLives;

        myStats.lives = _livesRemaining;
        myStats.kills = 0;
        myStats.damageDealt = 0;
        myStats.damageTaken = 0;

        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);

        return this;
    }

    protected override void Start()
    {
        base.Start();
        _myInputManager = new InputManager(ID);
        _myIndicator = transform.Find("UI Elements").Find("Indicator").GetComponent<SpriteRenderer>();
        _myIndicator.sprite = Resources.Load<Sprite>(Glob.PlayerIndicatorBase + (ID + 1).ToString());

        _myCirclePointer = transform.Find("UI Elements").Find("PointerPivot");
        _myCirclePointer.gameObject.SetActive(false);
        _jumpParticle = GetComponent<ParticleSystem>();
        _castingParticle = transform.Find("Casting").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isDead)
        {
            Move(true);
        }
        handleRespawn();
        //HandleCollision();
    }

    void Update()
    {
        if (!_isDead)
        {
            Move(false);
            HandleAimPointer();
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += 1;
                if (_manaRemaining < 100)
                {
                    _manaRemaining += Glob.ManaIncreasePerSecond;
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
                }
                if (IsInSteamCloud)
                {
                    TakeDamage((int)steamCloudDamage);
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
                    Debug.Log("Damage in steam cloud");
                }
            }
            if (_isSwitchingElement && Time.time >= _changedElementTime + _changedElementDelay)
            {
                SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, false, _firstElement, false);
                _isSwitchingElement = false;
            }
            else if (_hasSwitchedElement && Time.time >= _changedElementTime + _changedElementDelay)
            {
                SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, false, _firstElement, false);
                SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, true, _secondElement, false);
                _hasSwitchedElement = false;
            }
            if (_myInputManager.GetButtonDownJump())
            {
                jump();
            }
            if (_myInputManager.GetButtonDownSpellCast1() && _castingSpell == false)
            {
                launchSpell(_firstElement, _secondElement);
            }
            if (_myInputManager.GetButtonDownSpellCast2() && _castingSpell == false)
            {
                launchSpell(_firstElement, _secondElement, true);
            }
            if (_myInputManager.GetButtonFireElement())
            {
                if (!_isSwitchingElement)
                {
                    ChangeElement(SpellDatabase.Element.Fire, 0);
                }
                else
                {
                    ChangeElement(SpellDatabase.Element.Fire, 1);
                }
            }
            if (_myInputManager.GetButtonWaterElement())
            {
                if (!_isSwitchingElement)
                {
                    ChangeElement(SpellDatabase.Element.Water, 0);
                }
                else
                {
                    ChangeElement(SpellDatabase.Element.Water, 1);
                }
            }
            if (_myInputManager.GetButtonEarthElement())
            {
                if (!_isSwitchingElement)
                {
                    ChangeElement(SpellDatabase.Element.Earth, 0);
                }
                else
                {
                    ChangeElement(SpellDatabase.Element.Earth, 1);
                }
            }
        }
        else
        {
            _grounded = isGrounded();
            if (_rb.velocity.x < 0.1f && _rb.velocity.x > -0.1f && _castingSpell == false)
            {
                _disableMovement = false;
            }
            if (_myInputManager.GetAxisMoveHorizontal() != 0 && _disableMovement == false)
            {
                _rb.velocity = new Vector2(_myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed * Mathf.Min(Mathf.Abs(_rb.velocity.x / 2) + 0.5f, 1), _rb.velocity.y);
                //Move horizontally.
            }
            if (_myInputManager.GetAxisMoveVertical() != 0)
            {
                //Move vertically. (idk when that would be, ladders or something? Just a placeholder.)
            }
            if (_myInputManager.GetButtonJump() && _rb.velocity.y > 0)
            {
                jumpContinuous();
            }
        }
    }

    private void HandleAimPointer()
    {
        float xAxis = _myInputManager.GetAxisLookHorizontal();
        float yAxis = _myInputManager.GetAxisLookVertical();

        //If there's input
        if (!(xAxis == 0 && yAxis == 0))
        {
            if (!_myCirclePointer.gameObject.activeSelf)
            {
                _myCirclePointer.gameObject.SetActive(true);
            }

            Vector2 vec2 = new Vector2(0, 1);
            Vector2 vec0 = new Vector2(xAxis, yAxis);
            vec0.Normalize();
            //_myCirclePointer.transform.eulerAngles = vec0;

            if (vec0.x > 0)
            {
                vec2 = new Vector2(0, -1);
                float Degrees = Vector2.Angle(vec2, vec0);
                _myCirclePointer.transform.eulerAngles = new Vector3(_myCirclePointer.transform.eulerAngles.x, _myCirclePointer.transform.eulerAngles.y, Degrees);
            }
            else
            {
                float Degrees = Vector2.Angle(vec2, vec0);
                _myCirclePointer.transform.eulerAngles = new Vector3(_myCirclePointer.transform.eulerAngles.x, _myCirclePointer.transform.eulerAngles.y, Degrees + 180);
            }
        }
        else if (_myCirclePointer.gameObject.activeSelf)
        {
            _myCirclePointer.gameObject.SetActive(false);
        }
    }

    private void ChangeElement(SpellDatabase.Element pElement, int pElementSlot)
    {
        if (pElementSlot == 0)
        {
            _changedElementTime = Time.time;
            _firstElement = pElement;
            SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
            SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, false, _firstElement, true);
            SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, true, _secondElement, false);
            _isSwitchingElement = true;
        }
        else if (pElementSlot == 1)
        {
            _changedElementTime = Time.time;
            _secondElement = pElement;
            SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
            SceneManager.GetInstance().GetCurrentArena().GlowBackgroundPlayerBannerElement(ID, true, _secondElement, true);
            _isSwitchingElement = false;
            _hasSwitchedElement = true;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.isTrigger)
        {
            HandleCollision(collision);
        }
    }
    protected override void HandleCollision(Collision2D collision)
    {

    }
    private void handleRespawn()
    {
        if (_isDead)
        {
            if (Time.time - _deathTime >= Glob.respawnDelay)
            {
                respawn();
            }
        }
        if (_invulnerable)
        {
            if (Time.time - _invulnerableStartTime >= Glob.maxInvulnerableTime)
            {
                setInvulnerable(false);
            }
        }
    }

    private void jump()
    {
        if (!_disableMovement)
        {
            if (_grounded)
            {
                _jumpParticle.Play();
                _castingParticle.Stop();
                _rb.AddForce(new Vector2(0, Glob.jumpHeight), ForceMode2D.Impulse);
                if (ID == 0)
                {
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Backgroundsong/Backgroundtrack");
                    SoundManager.GetInstance().PlaySound(Glob.Player1JumpSound);
                }
                if (ID == 1)
                {
                    SoundManager.GetInstance().PlaySound(Glob.Player2JumpSound);
                }

                _jumpTime = Time.time;
            }
            else if (!_usedDoubleJump)
            {
                _jumpParticle.Play();
                _castingParticle.Stop();
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(new Vector2(0, Glob.jumpDoubleHeight), ForceMode2D.Impulse);
                _jumpTime = Time.time;
                _usedDoubleJump = true;
                if (ID == 0)
                {
                    //FMODUnity.RuntimeManager.PlayOneShot("event:/Backgroundsong/Backgroundtrack");
                    SoundManager.GetInstance().PlaySound(Glob.Player1JumpSound);
                }
                if (ID == 1)
                {
                    SoundManager.GetInstance().PlaySound(Glob.Player2JumpSound);
                }
            }
           
        }
    }
    private void jumpContinuous()
    {
        if (_usedDoubleJump)
        {
            if (Time.time - _jumpTime <= Glob.jumpDoubleTimeContinuous)
            {
                _rb.AddForce(new Vector2(0, Glob.jumpDoubleHeightContinuous * (1 - ((Time.time - _jumpTime) / Glob.jumpDoubleTimeContinuous))));
            }
        }
        else
        {
            if (Time.time - _jumpTime <= Glob.jumpTimeContinuous)
            {
                _rb.AddForce(new Vector2(0, Glob.jumpHeightContinuous * (1 - ((Time.time - _jumpTime) / Glob.jumpTimeContinuous))));
            }
        }
    }

    private bool isGrounded()
    {
        int layerMask = 1 << 9;
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up * 0.1f, Color.yellow, 1);
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) + (Vector3.right * _coll.bounds.extents.x), -Vector2.up * 0.1f, Color.yellow, 1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) + (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f, layerMask);
        if (hit.transform != null)
        {
            if (hit.transform.GetComponent<PlatformEffector2D>().useOneWay)
            {
                if (_rb.velocity.y <= 0)
                {
                    _usedDoubleJump = false;
                    return true;
                }
            }
            else
            {
                _usedDoubleJump = false;
                return true;
            }
        }
        if (hit2.transform != null)
        {
            if (hit2.transform.GetComponent<PlatformEffector2D>().useOneWay)
            {
                if (_rb.velocity.y <= 0)
                {
                    _usedDoubleJump = false;
                    return true;
                }
            }
            else
            {
                _usedDoubleJump = false;
                return true;
            }
        }
        return false;
    }

    private Spell launchSpell(SpellDatabase.Element firstEle, SpellDatabase.Element secondEle, bool reversed = false)
    {
        Spell launchedspell = SpellDatabase.GetInstance().GetSpell(firstEle, secondEle);

        if (_manaRemaining >= launchedspell.GetManaCost())
        {
            if (reversed)
            {
                launchedspell = SpellDatabase.GetInstance().GetSpell(secondEle, firstEle);
            }
            PlaySpellSound(launchedspell);
            float xAxis = _myInputManager.GetAxisLookHorizontal();
            float yAxis = _myInputManager.GetAxisLookVertical();

            //If there's no input, should do forward.
            if (xAxis == 0 && yAxis == 0)
            {
                xAxis = 1;
            }
            Vector2 vec2 = new Vector2(0, 1);
            Vector2 vec0 = new Vector2(xAxis, yAxis);
            vec0.Normalize();

            Debug.DrawRay(this.transform.position, new Vector3(vec0.x * 2, vec0.y * 2, 0), Color.red, 5f);
            int layerMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), vec0, 2f, layerMask);
            if (!hit)
            {
                StartCoroutine(SpawnSpell(launchedspell, vec0, vec2));
                HandleCastingBehaviour();
                if (reversed)
                {
                    SceneManager.GetInstance().GetCurrentArena().GlowPlayerBannerElement(ID, reversed, secondEle, true);
                }
                else
                {
                    SceneManager.GetInstance().GetCurrentArena().GlowPlayerBannerElement(ID, reversed, firstEle, true);
                }
            }
        }
        return launchedspell;
    }

    IEnumerator SpawnSpell(Spell pSpell, Vector2 pInput, Vector2 pNullPoint)
    {

        float castTime = pSpell.GetCastTime();
        _manaRemaining -= (int)pSpell.GetManaCost();
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);

        yield return new WaitForSeconds(castTime);

        if (pInput != new Vector2(_myInputManager.GetAxisLookHorizontal(), _myInputManager.GetAxisLookVertical()) && (_myInputManager.GetAxisLookHorizontal() != 0 && _myInputManager.GetAxisLookVertical() != 0))
        {
            pInput = new Vector2(_myInputManager.GetAxisLookHorizontal(), _myInputManager.GetAxisLookVertical());
            pInput.Normalize();
        }
        Debug.DrawRay(this.transform.position, new Vector3(pInput.x * 2, pInput.y * 2, 0), Color.blue, 5f);
        _castingParticle.Stop();
        _castingSpell = false;
        Vector3 position = this.transform.position + new Vector3(pInput.x * Glob.spellOffset, pInput.y * Glob.spellOffset, 0);
        if (pSpell is EarthWaterSpell)
        {
            position = this.transform.position + new Vector3(Math.Sign(pInput.x) * Glob.spellOffset, 0, 0);
        }
        Spell launchedspelltest = Instantiate(pSpell, position, new Quaternion());
        launchedspelltest.SetPlayer(this);

        SceneManager.GetInstance().GetCurrentArena().GlowPlayerBannerElement(ID, false, _firstElement, false);
        SceneManager.GetInstance().GetCurrentArena().GlowPlayerBannerElement(ID, true, _secondElement, false);

        if (pInput.x > 0)
        {
            pNullPoint = new Vector2(0, -1);
            float Degrees = Vector2.Angle(pNullPoint, pInput);
            launchedspelltest.transform.eulerAngles = new Vector3(launchedspelltest.transform.eulerAngles.x, launchedspelltest.transform.eulerAngles.y, Degrees);
        }
        else
        {
            float Degrees = Vector2.Angle(pNullPoint, pInput);
            launchedspelltest.transform.eulerAngles = new Vector3(launchedspelltest.transform.eulerAngles.x, launchedspelltest.transform.eulerAngles.y, Degrees + 180);
        }

        if (pSpell is EarthEarthSpell)
        {
            launchedspelltest.transform.parent = transform;
        }
        if (pSpell is WaterWaterSpell)
        {
            launchedspelltest.transform.position = this.transform.position;
            WaterWaterSpell waterWaterSpell = launchedspelltest as WaterWaterSpell;
            waterWaterSpell.SetPlayerCaster(this);
        }
        if (pSpell is FireWaterSpell)
        {
            launchedspelltest.transform.position = this.transform.position;
            FireWaterSpell fireWaterSpell = launchedspelltest as FireWaterSpell;
            fireWaterSpell.SetPlayerCaster(this);
        }
        if (pSpell is EarthWaterSpell)
        {
            EarthWaterSpell earthWaterSpell = launchedspelltest as EarthWaterSpell;
            earthWaterSpell.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void PlaySpellSound(Spell pSpell)
    {
        int random = UnityEngine.Random.Range(0, Glob.randomAttackSoundChance);
        if(random == 1)
        {
            if(ID == 0)
            {
                SoundManager.GetInstance().PlaySound(Glob.Player1AttackSound);
            } if(ID == 1)
            {
                SoundManager.GetInstance().PlaySound(Glob.Player2AttackSound);
            }
        }
        if (pSpell is FireFireSpell)
        {
            RuntimeManager.PlayOneShot(Glob.FireBeamSound);
        }
        if (pSpell is FireWaterSpell)
        {
            RuntimeManager.PlayOneShot(Glob.SteamcircleSound);
        }
        if (pSpell is FireEarthSpell)
        {
            RuntimeManager.PlayOneShot(Glob.MeteorThrowSound);
        }
        if (pSpell is WaterFireSpell)
        {
            RuntimeManager.PlayOneShot(Glob.WaterballSound);
        }
        if (pSpell is WaterWaterSpell)
        {
            RuntimeManager.PlayOneShot(Glob.WaterblastSound);
        }
        if(pSpell is WaterEarthSpell)
        {
            RuntimeManager.PlayOneShot(Glob.SnowballCastSound);
        }
        if (pSpell is EarthFireSpell)
        {
            RuntimeManager.PlayOneShot(Glob.FirerockThrowSound);
        }
        if (pSpell is EarthWaterSpell)
        {
            RuntimeManager.PlayOneShot(Glob.EarthPillarsSound);
        }
        if (pSpell is EarthEarthSpell)
        {
            FMODUnity.RuntimeManager.PlayOneShot(Glob.AvalancheChargeSound);
        }
    }

    //This is for loading animations etc.
    private void HandleCastingBehaviour()
    {
        _castingParticle.Play();
        _castingSpell = true;

    }

    private void handleLives()
    {
        if (_livesRemaining > 0)
        {
            if (_healthRemaining <= 0)
            {
                _livesRemaining--;
                myStats.lives = _livesRemaining;
                setIsDead(true);
                if (_livesRemaining == 1)
                {
                    SoundManager.GetInstance().SetBackGroundMusicIntensity(0.65f);
                }
                if (_livesRemaining <= 0)
                {
                    Debug.Log("Player is out of lives.");
                    gameObject.SetActive(false);
                    SoundManager.GetInstance().PlaySound(Glob.GameOverSound);
                    SceneManager.GetInstance().PlayerDown(); //TODO: End after one player remains, not when the first one falls.

                    //Dead
                }
                //respawn();
            }
            SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
        }
    }

    private void setInvulnerable(bool toggle)
    {
        _invulnerable = toggle;
        if (_invulnerable)
        {
            _invulnerableStartTime = Time.time;
            GetComponent<MeshRenderer>().material.color = Color.blue; //TODO: Improve invulnerability feedback.
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    private void setIsDead(bool toggle)
    {
        _isDead = toggle;
        GetComponent<MeshRenderer>().enabled = !_isDead;
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = _isDead;
        _coll.enabled = !_isDead;
        _myCirclePointer.gameObject.SetActive(false);
        if (_isDead)
        {
            _deathTime = Time.time;
            GameObject deathParticle = Instantiate(Resources.Load<GameObject>(Glob.DeathParticle), transform.position, new Quaternion()); //TODO: Improve reuse, instead of instantiating
        }
    }

    private void respawn()
    {
        setIsDead(false);
        setInvulnerable(true);
        _healthRemaining = Glob.maxHealth;
        _manaRemaining = Glob.maxMana;
        _rb.velocity = Vector3.zero;
        transform.position = SceneManager.GetInstance().GetCurrentArena().GetRandomRespawnPoint();
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
    }

    public void HandleSpellHit(Spell hit, int pKnockback, int pDamage, Vector2 pHitAngle)
    {
        if (!_isDead && !_invulnerable)
        {
            int startingHealth = _healthRemaining;
            int startingLives = _livesRemaining;
            _rb.velocity = new Vector2(pHitAngle.x * pKnockback, pHitAngle.y * pKnockback);
            TakeDamage(pDamage);
            if (hit.GetPlayer() != this)
            {
                hit.GetPlayer().AddDamageDealt(startingHealth - _healthRemaining);
                if (_livesRemaining < startingLives)
                {
                    hit.GetPlayer().AddKill();
                }
            }
            _disableMovement = true;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!_isDead && dmg != 0)
        {
            if (ID == 0 && dmg != Glob.maxHealth)
            {
                SoundManager.GetInstance().PlaySound(Glob.Player1HurtSound);
            }
            if (ID == 1 && dmg != Glob.maxHealth)
            {
                SoundManager.GetInstance().PlaySound(Glob.Player2HurtSound);
            }
            Debug.Log("Taking damage " + dmg);
            myStats.damageTaken += Mathf.Min(_healthRemaining, dmg);
            _healthRemaining -= dmg;
            handleLives();
        }
    }

    public void AddDamageDealt(int dmg)
    {
        myStats.damageDealt += dmg;
    }
    public void AddKill()
    {
        myStats.kills++;
    }

    public void SetSteamCloud(float pDamage, bool pBool)
    {
        IsInSteamCloud = pBool;
        steamCloudDamage = pDamage;
    }

    public int GetID()
    {
        return ID;
    }

    public PlayerStats GetStats()
    {
        return myStats;
    }
}
