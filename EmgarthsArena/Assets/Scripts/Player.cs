using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager myInputManager;
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
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update () {
        Move();
        HandleCollision();
	}

    protected override void Move()
    {
        if (Input.GetAxis(myInputManager.GetMoveAxisHorizontal()) != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxis(myInputManager.GetMoveAxisHorizontal()), 0));
            //Move horizontally.
        }
        if (Input.GetAxis(myInputManager.GetMoveAxisVertical()) != 0)
        {
            //Move vertically. (idk when that would be, ladders or something? Just a placeholder.)
        }
        if (Input.GetButtonDown(myInputManager.GetJumpButton()))
        {
            jump();
        }
        if (Input.GetButtonDown(myInputManager.GetSpellCastButton1()))
        {
            launchSpell(firstElement, secondElement);
        }
        if (Input.GetButtonDown(myInputManager.GetSpellCastButton2()))
        {
            launchSpell(secondElement, firstElement);
        }
    }

    protected override void HandleCollision()
    {

    }

    private void jump()
    {
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    private Spell launchSpell(SpellDatabase.Element firstEle, SpellDatabase.Element secondEle)
    {
        return SpellDatabase.GetInstance().GetSpell(firstEle, secondEle);
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
