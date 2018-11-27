using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject
{

    private InputManager _myInputManager;
    private int _healthRemaining;
    private int _livesRemaining;
    private SpellDatabase.Element _firstElement;
    private SpellDatabase.Element _secondElement;

    private Transform _myMagicCircleLeft;
    private Transform _myMagicCircleRight;
    private Transform _myCirclePointer;
    private bool _changingElement1 = false;
    private bool _changingElement2 = false;
    private bool _disableMovement = false;
    private bool _castingSpell = false;

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
        _healthRemaining = Glob.maxHealth;
        _livesRemaining = Glob.maxLives;
        return this;
    }

    protected override void Start()
    {
        base.Start();
        _myInputManager = new InputManager(ID);
        _myMagicCircleLeft = transform.Find("UI Elements").Find("MagicCircleLeft");
        _myMagicCircleLeft.gameObject.SetActive(false);
        _myMagicCircleRight = transform.Find("UI Elements").Find("MagicCircleRight");
        _myMagicCircleRight.gameObject.SetActive(false);
        _myCirclePointer = transform.Find("UI Elements").Find("PointerPivot");
        _myCirclePointer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(true);
        HandleCollision();
    }

    void Update()
    {
        Move(false);
        if (_changingElement1 || _changingElement2)
        {
            changeElement();
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (_myInputManager.GetButtonDownJump() && _castingSpell == false)
            {
                jump();
            }
            if (_myInputManager.GetButtonDownSpellCast1() && _castingSpell == false)
            {
                launchSpell(_firstElement, _secondElement);
            }
            if (_myInputManager.GetButtonDownSpellCast2() && _castingSpell == false)
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
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement);
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
                    SceneManager.GetInstance().GetCurrentArena().UpdatePlayerBanner(ID, _firstElement, _secondElement);
                }
                _changingElement2 = false;
                _myMagicCircleRight.gameObject.SetActive(false);
                _myCirclePointer.gameObject.SetActive(false);
            }
        }
        else
        {
            _grounded = isGrounded();
            if (_rb.velocity.x < 0.1f && _rb.velocity.x > -0.1f && _castingSpell == false) _disableMovement = false;
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

    }

    private void jump()
    {
        if (_grounded)
        {
            GetComponent<ParticleSystem>().Play();
            _rb.AddForce(new Vector2(0, Glob.jumpHeight), ForceMode2D.Impulse);
            _jumpTime = Time.time;
        }
        else if (!_usedDoubleJump)
        {
            GetComponent<ParticleSystem>().Play();
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, Glob.jumpDoubleHeight), ForceMode2D.Impulse);
            _usedDoubleJump = true;
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
        if ((Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f) || Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y) + (Vector3.right * _coll.bounds.extents.x), -Vector2.up, 0.1f)) && _rb.velocity.y <= 0)
        {
            _usedDoubleJump = false;
            return true;
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

    IEnumerator SpawnSpell(Spell pSpell,  Vector2 pInput, Vector2 pNullPoint)
    { 
        yield return new WaitForSeconds(pSpell.GetCastTime());
        _disableMovement = false;
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
        }
        else
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
            //Left
            if (angle <= 245f && angle >= 202.5f)
            {
                newElement = SpellDatabase.Element.Earth;
                //Debug.Log("Earth - Left");
            }
            else if (angle <= 202.5f && angle >= 157.5f)
            {
                newElement = SpellDatabase.Element.Water;
                //Debug.Log("Water - Left");
            }
            else if (angle <= 157.5f && angle >= 115f)
            {
                newElement = SpellDatabase.Element.Fire;
                //Debug.Log("Fire - Left");
            }
        }
        else if (_changingElement2)
        {
            //Right
            if (angle <= 337.5f && angle >= 295f)
            {
                newElement = SpellDatabase.Element.Earth;
                //Debug.Log("Earth - Right");
            }
            else if ((angle <= 360f && angle >= 337.5f) || (angle <= 22.5f && angle >= 0))
            {
                newElement = SpellDatabase.Element.Water;
                //Debug.Log("Water - Right");
            }
            else if (angle <= 65f && angle >= 22.5f)
            {
                newElement = SpellDatabase.Element.Fire;
                //Debug.Log("Fire - Right");
            }
        }

        return newElement;
    }

    private void handleLives()
    {

    }

    private void respawn()
    {
        _livesRemaining--;
        if (_livesRemaining <= 0)
        {
            Debug.Log("Player is dead.");
            //Dead
        }
        else
        {
            _rb.velocity = Vector3.zero;
            transform.position = SceneManager.GetInstance().GetCurrentArena().GetRandomRespawnPoint();
        }
    }

    public void HandleSpellHit(Spell hit, int pKnockback, int pDamage, Vector2 pHitAngle)
    {
        Debug.Log(this.name + " is hit by :" + hit + " knockback:" + pKnockback + " damage:" + pDamage);
        _rb.velocity = new Vector2(pHitAngle.x * pKnockback, pHitAngle.y * pKnockback);
        _disableMovement = true;
    }

    public void TakeDamage(int dmg)
    {

    }
}
