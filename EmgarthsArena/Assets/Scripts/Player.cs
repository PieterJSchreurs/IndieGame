﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager myInputManager = new InputManager();
    private int healthRemaining;
    private int livesRemaining;
    private SpellDatabase.Element firstElement;
    private SpellDatabase.Element secondElement;
    private Rigidbody2D rb;

    public Player()
    {

    }

	// Use this for initialization
	void Start () {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        myInputManager = new InputManager();
        jump();
    }

    // Update is called once per frame
    void FixedUpdate () {
        Move();
        HandleCollision();
	}

    protected override void Move()
    {
        if (myInputManager.GetAxisMoveHorizontal() != 0)
        {
            //_rb.velocity = new Vector2(myInputManager.GetAxisMoveHorizontal() * Glob.playerAcceleration, _rb.velocity.y);
            _rb.AddForce(new Vector2(myInputManager.GetAxisMoveHorizontal() * Glob.playerAcceleration, 0));
            //rb.AddForce(new Vector2(myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed, 0));
            //transform.position += new Vector3(myInputManager.GetAxisMoveHorizontal() * Glob.playerSpeed * Time.deltaTime, 0, 0);
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
        _rb.velocity = new Vector2(_rb.velocity.x, Glob.jumpHeight);
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
        launchedspell = Instantiate(launchedspell, position, new Quaternion());
        launchedspell.transform.LookAt(this.transform);
        launchedspell.transform.eulerAngles = new Vector3(launchedspell.transform.eulerAngles.x + 90, launchedspell.transform.eulerAngles.y, launchedspell.transform.eulerAngles.z);
        
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
