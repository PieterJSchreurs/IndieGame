using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager _myInputManager;
    private int _healthRemaining;
    private int _livesRemaining;
    private bool _isDead = false;
    private float _deathTime = 0;
    private bool _invulnerable = false;
    private float _invulnerableStartTime = 0;
    private SpellDatabase.Element _firstElement;
    private SpellDatabase.Element _secondElement;

    private Transform _myMagicCircleLeft;
    private SpriteRenderer[] elementIconsLeft;
    private Transform _myMagicCircleRight;
    private SpriteRenderer[] elementIconsRight;
    private Transform _myCirclePointer;
    private bool _changingElement1 = false;
    private bool _changingElement2 = false;
    private bool _disableMovement = false;
    private bool _castingSpell = false;
    public ParticleSystem _castingParticle;
    public ParticleSystem _jumpParticle;

    private bool _grounded = false;
    private bool _usedDoubleJump = false;
    private float _jumpTime = 0;
    public int ID;


    public Player()
    {

    }

    public Player GetPlayer(int pPlayerIndex)
    {
        ID = pPlayerIndex;
        Debug.Log("Added player with ID: " + pPlayerIndex);
        _healthRemaining = Glob.maxHealth;
        _livesRemaining = Glob.maxLives;
        return this;
    }

    protected override void Start()
    {
        base.Start();
        _myInputManager = new InputManager(ID);
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
    void FixedUpdate () {
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
            if (Input.GetKeyDown(KeyCode.O))
            {
                TakeDamage(20);
            }

            Move(false);
            if (_changingElement1 || _changingElement2)
            {
                changeElement();
            }
        }
       // Debug.Log(_firstElement + " - " + _secondElement);
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (_myInputManager.GetButtonDownJump())
            {
                jump();
            }
            if (_myInputManager.GetButtonDownSpellCast1() > 0.8f && _castingSpell == false)
            {
                launchSpell(_firstElement, _secondElement);
            }
            if (_myInputManager.GetButtonDownSpellCast2() > 0.8f && _castingSpell == false)
            {
                launchSpell(_secondElement, _firstElement);
            }
            if (_myInputManager.GetButtonDownElementChange1() && !_changingElement2)
            {
                _changingElement1 = true;
                _myMagicCircleLeft.gameObject.SetActive(true);
                _myCirclePointer.gameObject.SetActive(true);
            }
            else if (_myInputManager.GetButtonDownElementChange2() && !_changingElement1)
            {
                _changingElement2 = true;
                _myMagicCircleRight.gameObject.SetActive(true);
                _myCirclePointer.gameObject.SetActive(true);
            }
            if (_myInputManager.GetButtonUpElementChange1() && !_changingElement2)
            {
                if (changeElement() != SpellDatabase.Element.Null)
                {
                    _firstElement = changeElement();
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining);
                }
                _changingElement1 = false;
                _myMagicCircleLeft.gameObject.SetActive(false);
                _myCirclePointer.gameObject.SetActive(false);
            }
            else if (_myInputManager.GetButtonUpElementChange2() && !_changingElement1)
            {
                if (changeElement() != SpellDatabase.Element.Null)
                {
                    _secondElement = changeElement();
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining);
                }
                _changingElement2 = false;
                _myMagicCircleRight.gameObject.SetActive(false);
                _myCirclePointer.gameObject.SetActive(false);
            }
        }
        else
        {
            _grounded = isGrounded();
            //if (_rb.velocity.x < 0.1f && _rb.velocity.x > -0.1f && _castingSpell == false) _disableMovement = false;
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
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up*0.1f, Color.yellow, 1);
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
            } else
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

    private Spell launchSpell(SpellDatabase.Element firstEle, SpellDatabase.Element secondEle)
    {
        Spell launchedspell = SpellDatabase.GetInstance().GetSpell(firstEle, secondEle);

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

        StartCoroutine(SpawnSpell(launchedspell, vec0, vec2));
        HandleCastingBehaviour();
        return launchedspell;
    }

    IEnumerator SpawnSpell(Spell pSpell, Vector2 pInput, Vector2 pNullPoint)
    {
        yield return new WaitForSeconds(pSpell.GetCastTime());
        _disableMovement = false;
        _castingParticle.Stop();
        _castingSpell = false;
        Vector3 position = this.transform.position + new Vector3(pInput.x * Glob.spellOffset, pInput.y * Glob.spellOffset, 0);
        Spell launchedspelltest = Instantiate(pSpell, position, new Quaternion());

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
    }

    //This is for loading animations etc.
    private void HandleCastingBehaviour()
    {        
        _castingParticle.Play();
        _castingSpell = true;
        _disableMovement = true;
    }


    private SpellDatabase.Element changeElement()
    {
        //Aim arrow using right stick
        //After aiming at an element for ... seconds, change the element (first or second based on element aimed at).
        //Disable magic circle

        float xAxis = _myInputManager.GetAxisLookHorizontal();
        float yAxis = _myInputManager.GetAxisLookVertical();

        //If there's no input, should do forward.
        if (xAxis == 0 && yAxis == 0)
        {
            yAxis = 1;
            _myCirclePointer.gameObject.SetActive(false);
        } else
        {
            _myCirclePointer.gameObject.SetActive(true);
        }

        //Determine where the player looks at.
        //Give the pointer a rotation from where he looks at.
        Vector2 vec2 = new Vector2(0, 1);
        Vector2 vec0 = new Vector2(xAxis, yAxis);

        if (xAxis > 0)
        {
            vec2 = new Vector2(0, -1);
            float Degrees = Vector2.Angle(vec2, vec0);
            _myCirclePointer.transform.eulerAngles = new Vector3(_myCirclePointer.transform.eulerAngles.x, _myCirclePointer.transform.eulerAngles.y, Degrees - 90);
        }
        else
        {
            float Degrees = Vector2.Angle(vec2, vec0);
            _myCirclePointer.transform.eulerAngles = new Vector3(_myCirclePointer.transform.eulerAngles.x, _myCirclePointer.transform.eulerAngles.y, Degrees + 90);
        }

        float angle = _myCirclePointer.transform.eulerAngles.z;
        SpellDatabase.Element newElement = SpellDatabase.Element.Null;
        if (_changingElement1)
        {
            elementIconsLeft[0].sprite = Resources.Load<Sprite>(Glob.FireElementIcon);
            elementIconsLeft[1].sprite = Resources.Load<Sprite>(Glob.WaterElementIcon);
            elementIconsLeft[2].sprite = Resources.Load<Sprite>(Glob.EarthElementIcon);

            //Left
            if (angle <= 245f && angle >= 202.5f)
            {
                newElement = SpellDatabase.Element.Earth;
                elementIconsLeft[2].sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                //Debug.Log("Earth - Left");
            }
            else if (angle <= 202.5f && angle >= 157.5f)
            {
                newElement = SpellDatabase.Element.Water;
                elementIconsLeft[1].sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                //Debug.Log("Water - Left");
            }
            else if (angle <= 157.5f && angle >= 115f)
            {
                newElement = SpellDatabase.Element.Fire;
                elementIconsLeft[0].sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                //Debug.Log("Fire - Left");
            }
        } else if (_changingElement2) {

            elementIconsRight[0].sprite = Resources.Load<Sprite>(Glob.FireElementIcon);
            elementIconsRight[1].sprite = Resources.Load<Sprite>(Glob.WaterElementIcon);
            elementIconsRight[2].sprite = Resources.Load<Sprite>(Glob.EarthElementIcon);

            //Right
            if (angle <= 337.5f && angle >= 295f)
            {
                newElement = SpellDatabase.Element.Earth;
                elementIconsRight[2].sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                //Debug.Log("Earth - Right");
            }
            else if ((angle <= 360f && angle >= 337.5f) || (angle <= 22.5f && angle >= 0))
            {
                newElement = SpellDatabase.Element.Water;
                elementIconsRight[1].sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                //Debug.Log("Water - Right");
            }
            else if (angle <= 65f && angle >= 22.5f)
            {
                newElement = SpellDatabase.Element.Fire;
                elementIconsRight[0].sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                //Debug.Log("Fire - Right");
            }
        }

        return newElement;
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
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining);
    }

    private void setInvulnerable(bool toggle)
    {
        _invulnerable = toggle;
        if (_invulnerable)
        {
            _invulnerableStartTime = Time.time;
            GetComponent<MeshRenderer>().material.color = Color.blue; //TODO: Improve invulnerability feedback.
        } else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    private void setIsDead(bool toggle)
    {
        _isDead = toggle;
        GetComponent<MeshRenderer>().enabled = !_isDead; //TODO: Very ugly way of doing this, improve it.
        _changingElement1 = false;
        _changingElement2 = false;
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
        SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement, _healthRemaining, 100, _livesRemaining);
    }

    public void HandleSpellHit(Spell hit, int pKnockback, int pDamage, Vector2 pHitAngle)
    {
        Debug.Log(this.name + " is hit by :" + hit + " knockback:" + pKnockback + " damage:" + pDamage);
        _rb.velocity = new Vector2(pHitAngle.x * pKnockback, pHitAngle.y * pKnockback);
        _disableMovement = true;
    }
    public void TakeDamage(int dmg)
    {
        _healthRemaining -= dmg;
        handleLives();
    }
}
