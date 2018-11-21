using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager myInputManager = new InputManager();
    private int healthRemaining;
    private int livesRemaining;
    private SpellDatabase.Element firstElement;
    private SpellDatabase.Element secondElement;

    private bool _grounded = false;
    private bool _usedDoubleJump = false;

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
        Move();
        HandleCollision();
	}

    protected override void Move()
    {
        _grounded = isGrounded();

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rb.AddForce(new Vector2(1500, 1500), ForceMode2D.Impulse);
        }

        if (myInputManager.GetAxisMoveHorizontal() != 0)
        {
            _rb.velocity = new Vector2(myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed, _rb.velocity.y);
            //Move horizontally.
        }
        if (myInputManager.GetAxisMoveVertical() != 0)
        {
            //Move vertically. (idk when that would be, ladders or something? Just a placeholder.)
        }
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

    protected override void HandleCollision()
    {

    }

    private void jump()
    {
        if (_grounded)
        {
            GetComponent<ParticleSystem>().Play();
            _rb.AddForce(new Vector2(0, Glob.jumpHeight), ForceMode2D.Impulse);
        } else if (!_usedDoubleJump)
        {
            GetComponent<ParticleSystem>().Play();
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(new Vector2(0, Glob.jumpHeight * 0.75f), ForceMode2D.Impulse);
            _usedDoubleJump = true;
        }
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position - (Vector3.up * _coll.bounds.extents.y), -Vector2.up*0.1f, Color.yellow, 1);
        bool grounded = Physics2D.Raycast(transform.position - (Vector3.up * _coll.bounds.extents.y), -Vector2.up, 0.1f);
        if (grounded)
        {
            _usedDoubleJump = false;
        }
        return grounded;
    }

    private Spell launchSpell(SpellDatabase.Element firstEle, SpellDatabase.Element secondEle)
    {
        Debug.Log("Spell launched");
        Spell launchedspell = SpellDatabase.GetInstance().GetSpell(firstEle, secondEle);
        Debug.Log(launchedspell);
        float xAxis = myInputManager.GetAxisLookHorizontal();
        float yAxis = myInputManager.GetAxisLookVertical();
        float Offset = 0.5f;

        if (xAxis == 0 && yAxis == 0)
        {
            xAxis = 1;
        }
        //Get a spell from the spell database.
        //Determine where the player looks at.
        //Give the spell a rotation from where he looks at.
        //Add it to a position with an offset compared to the player.
        Vector3 position = transform.position + new Vector3(xAxis * Offset, yAxis * Offset, 0);
        //launchedspell = Instantiate(launchedspell, position, new Quaternion());
        //launchedspell.transform.LookAt(this.transform);
        //launchedspell.transform.eulerAngles = new Vector3(launchedspell.transform.eulerAngles.x + 90, launchedspell.transform.eulerAngles.y, launchedspell.transform.eulerAngles.z);
        
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

    }

    public void HandleSpellHit(Spell hit)
    {

    }

    public void TakeDamage(int dmg)
    {

    }
}
