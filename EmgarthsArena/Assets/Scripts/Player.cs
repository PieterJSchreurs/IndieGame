using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager myInputManager = new InputManager();
    private int healthRemaining;
    private int livesRemaining;
    private SpellDatabase.Element firstElement = SpellDatabase.Element.Fire;
    private SpellDatabase.Element secondElement = SpellDatabase.Element.Water;

    private bool _grounded = false;
    private bool _usedDoubleJump = false;
    private float _jumpTime = 0;

    public Player()
    {

    }

    protected override void Start()
    {
        base.Start();
        myInputManager = new InputManager();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move(true);
        HandleCollision();
	}

    void Update()
    {
        Move(false);
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)               
        {
            if (myInputManager.GetButtonDownJump())
            {
                jump();
            }
            if (myInputManager.GetButtonDownSpellCast1())
            {
                launchSpell(firstElement, secondElement);
            }
            if (myInputManager.GetButtonDownSpellCast2())
            {
                launchSpell(secondElement, firstElement);
            }
        }
        else
        {
            _grounded = isGrounded();

            if (myInputManager.GetAxisMoveHorizontal() != 0)
            {
                _rb.velocity = new Vector2(myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed, _rb.velocity.y);
                //Move horizontally.
            }
            if (myInputManager.GetAxisMoveVertical() != 0)
            {
                //Move vertically. (idk when that would be, ladders or something? Just a placeholder.)
            }
            if (myInputManager.GetButtonJump() && _rb.velocity.y > 0)
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
        } else if (!_usedDoubleJump)
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
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y) - (Vector3.right * _coll.bounds.extents.x), -Vector2.up*0.1f, Color.yellow, 1);
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

        float xAxis = myInputManager.GetAxisLookHorizontal();
        float yAxis = myInputManager.GetAxisLookVertical();
        float Offset = 2f;

        //If there's no input, should do forward.
        if (xAxis == 0 && yAxis == 0)
        {
            xAxis = 1;
        }
        //Get a spell from the spell database.
        //Determine where the player looks at.
        //Give the spell a rotation from where he looks at.
        //Add it to a position with an offset compared to the player.
        Vector3 position = this.transform.position + new Vector3(xAxis * Offset, yAxis * Offset, 0);
        Spell launchedspelltest = Instantiate(launchedspell, position, new Quaternion());
        Vector2 vec2 = new Vector2(0, 1);
        Vector2 vec0 = new Vector2(xAxis, yAxis);

        if (xAxis > 0)
        {
            vec2 = new Vector2(0, -1);
            float Degrees = Vector2.Angle(vec2, vec0);
            launchedspelltest.transform.eulerAngles = new Vector3(launchedspelltest.transform.eulerAngles.x, launchedspelltest.transform.eulerAngles.y, Degrees);
        }
        else
        {
            float Degrees = Vector2.Angle(vec2, vec0);
            launchedspelltest.transform.eulerAngles = new Vector3(launchedspelltest.transform.eulerAngles.x, launchedspelltest.transform.eulerAngles.y, Degrees + 180);
        }

        return launchedspell;
    }

    private void changeElement()
    {

    }

    private void handleLives()
    {

    }

    private void respawn()
    {
        Debug.Log("Respawn player");
    }

    public void HandleSpellHit(Spell hit)
    {

    }

    public void TakeDamage(int dmg)
    {

    }
}
