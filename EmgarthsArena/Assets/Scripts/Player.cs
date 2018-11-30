using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{

    private InputManager _myInputManager;
    private int _healthRemaining;
    private int _manaRemaining;
    private int _livesRemaining;
    private bool _isDead = false;
    private float _deathTime = 0;
    private bool _invulnerable = false;
    private float _invulnerableStartTime = 0;
    private SpellDatabase.Element _firstElement = SpellDatabase.Element.Fire;
    private SpellDatabase.Element _secondElement = SpellDatabase.Element.Water;

    private SpriteRenderer _myIndicator;
    private Transform _myMagicCircleLeft;
    private SpriteRenderer[] elementIconsLeft;
    private Transform _myMagicCircleRight;
    private SpriteRenderer[] elementIconsRight;
    private Transform _myCirclePointer;
    private bool _disableMovement = false;
    private bool _castingSpell = false;
    private ParticleSystem _castingParticle;
    private ParticleSystem _jumpParticle;
    private bool _isSwitchingElement = false;
    private float _changedElementTime;

    private bool _grounded = false;
    private bool _usedDoubleJump = false;
    private float _jumpTime = 0;
    private int ID;

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

        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);

        return this;
    }

    protected override void Start()
    {
        base.Start();
        _myInputManager = new InputManager(ID);
        _myIndicator = transform.Find("UI Elements").Find("Indicator").GetComponent<SpriteRenderer>();
        _myIndicator.sprite = Resources.Load<Sprite>(Glob.PlayerIndicatorBase + (ID + 1).ToString());
        _myMagicCircleLeft = transform.Find("UI Elements").Find("MagicCircleLeft");
        elementIconsLeft = new SpriteRenderer[3];
        elementIconsLeft[0] = _myMagicCircleLeft.Find("LeftSelection").Find("Fire").GetComponent<SpriteRenderer>(); //TODO: Better getter, in case we get more elements.
        elementIconsLeft[1] = _myMagicCircleLeft.Find("LeftSelection").Find("Water").GetComponent<SpriteRenderer>();
        elementIconsLeft[2] = _myMagicCircleLeft.Find("LeftSelection").Find("Earth").GetComponent<SpriteRenderer>();

        _myMagicCircleLeft.gameObject.SetActive(false);
        _myMagicCircleRight = transform.Find("UI Elements").Find("MagicCircleRight");
        elementIconsRight = new SpriteRenderer[3];
        elementIconsRight[0] = _myMagicCircleRight.Find("RightSelection").Find("Fire").GetComponent<SpriteRenderer>(); //TODO: Better getter, in case we get more elements.
        elementIconsRight[1] = _myMagicCircleRight.Find("RightSelection").Find("Water").GetComponent<SpriteRenderer>();
        elementIconsRight[2] = _myMagicCircleRight.Find("RightSelection").Find("Earth").GetComponent<SpriteRenderer>();
        _myMagicCircleRight.gameObject.SetActive(false);
        _myCirclePointer = transform.Find("UI Elements").Find("PointerPivot");
        _myCirclePointer.gameObject.SetActive(false);
        _jumpParticle = GetComponent<ParticleSystem>();
        _castingParticle = transform.Find("Casting").GetComponent<ParticleSystem>();

        Debug.Log(_jumpParticle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isDead)
        {
            Move(true);
        }
        HandleCollision();//TODO: Move invulnerable and death check to a better place.
    }

    void Update()
    {
        if (!_isDead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                launchSpell(_firstElement, _secondElement);
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                launchSpell(_firstElement, _secondElement, true);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_firstElement + 1 >= SpellDatabase.Element.Null)
                {
                    _firstElement = 0;
                }
                else
                {
                    _firstElement = _firstElement + 1;
                }
                Debug.Log(_firstElement + " - " + _secondElement);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (_secondElement + 1 >= SpellDatabase.Element.Null)
                {
                    _secondElement = 0;
                }
                else
                {
                    _secondElement = _secondElement + 1;
                }
                Debug.Log(_firstElement + " - " + _secondElement);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                TakeDamage(20);
            }

            Move(false);
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if(_isSwitchingElement == true && Time.time >= _changedElementTime+1.0f)
            {
                _isSwitchingElement = false;
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
                _rb.velocity = new Vector2(_myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed, _rb.velocity.y);
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

    private void ChangeElement(SpellDatabase.Element pElement, int pElementSlot)
    {
        if (pElementSlot == 0)
        {
            _changedElementTime = Time.time;
            _firstElement = pElement;
            SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
            _isSwitchingElement = true;
        }
        else if (pElementSlot == 1)
        {
            _secondElement = pElement;
            SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
            _isSwitchingElement = false;
        }
    }

    protected override void HandleCollision()
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
                _jumpTime = Time.time;
            }
            else if (!_usedDoubleJump)
            {
                _jumpParticle.Play();
                _castingParticle.Stop();
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(new Vector2(0, Glob.jumpDoubleHeight), ForceMode2D.Impulse);
                _usedDoubleJump = true;
            }
        }
    }
    private void jumpContinuous()
    {
        if (Time.time - _jumpTime <= Glob.jumpTimeContinuous)
        {
            _rb.AddForce(new Vector2(0, Glob.jumpHeightContinuous * (1 - ((Time.time - _jumpTime) / Glob.jumpTimeContinuous))));
        }
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up * 0.1f, Color.yellow, 1);
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) + (Vector3.right * _coll.bounds.extents.x), -Vector2.up * 0.1f, Color.yellow, 1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) + (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f);
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
        if (reversed)
        {
            launchedspell = SpellDatabase.GetInstance().GetSpell(secondEle, firstEle);
        }

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
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y), vec0, 2f);
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

        return launchedspell;
    }

    IEnumerator SpawnSpell(Spell pSpell, Vector2 pInput, Vector2 pNullPoint)
    {
        yield return new WaitForSeconds(pSpell.GetCastTime());
        if (pInput != new Vector2(_myInputManager.GetAxisLookHorizontal(), _myInputManager.GetAxisLookVertical()) && (_myInputManager.GetAxisLookHorizontal() != 0 && _myInputManager.GetAxisLookVertical() != 0))
        {
            pInput = new Vector2(_myInputManager.GetAxisLookHorizontal(), _myInputManager.GetAxisLookVertical());
            pInput.Normalize();
        }
        Debug.DrawRay(this.transform.position, new Vector3(pInput.x * 2, pInput.y * 2, 0), Color.blue, 5f);
        _castingParticle.Stop();
        _castingSpell = false;
        Vector3 position = this.transform.position + new Vector3(pInput.x * Glob.spellOffset, pInput.y * Glob.spellOffset, 0);
        Spell launchedspelltest = Instantiate(pSpell, position, new Quaternion());

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
    }

    //This is for loading animations etc.
    private void HandleCastingBehaviour()
    {
        _castingParticle.Play();
        _castingSpell = true;

    }

    private void handleLives()
    {
        Debug.Log("Health: " + _healthRemaining + "/" + Glob.maxHealth + ", Lives: " + _livesRemaining + "/" + Glob.maxLives);
        //SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining); //TODO: Make this work
        if (_healthRemaining <= 0)
        {
            _livesRemaining--;
            //SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining);
            setIsDead(true);
            if (_livesRemaining <= 0)
            {
                Debug.Log("Player is out of lives.");
                gameObject.SetActive(false);
                SceneManager.GetInstance().PlayerDown(); //TODO: End after one player remains, not when the first one falls.

                //Dead
            }
            //respawn();
        }
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
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
        GetComponent<MeshRenderer>().enabled = !_isDead; //TODO: Very ugly way of doing this, improve it.
        transform.position = new Vector3(28, 2, 0); //TODO: Ugly way of disabling the player.
        _myMagicCircleLeft.gameObject.SetActive(false);
        _myMagicCircleRight.gameObject.SetActive(false);
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
        _rb.velocity = Vector3.zero;
        transform.position = SceneManager.GetInstance().GetCurrentArena().GetRandomRespawnPoint();
        Debug.Log("Respawned! Health: " + _healthRemaining + "/" + Glob.maxHealth + ", Lives: " + _livesRemaining + "/" + Glob.maxLives);
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, _manaRemaining, _livesRemaining);
    }

    public void HandleSpellHit(Spell hit, int pKnockback, int pDamage, Vector2 pHitAngle)
    {
        _rb.velocity = new Vector2(pHitAngle.x * pKnockback, pHitAngle.y * pKnockback);
        TakeDamage(pDamage);
        _disableMovement = true;
    }
    public void TakeDamage(int dmg)
    {
        Debug.Log("Taking damage " + dmg);
        _healthRemaining -= dmg;
        handleLives();
    }
}
