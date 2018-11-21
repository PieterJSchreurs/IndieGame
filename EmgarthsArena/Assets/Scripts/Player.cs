using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    private InputManager myInputManager = new InputManager();
    private int healthRemaining;
    private int livesRemaining;
    private SpellDatabase.Element firstElement = SpellDatabase.Element.Fire;
    private SpellDatabase.Element secondElement = SpellDatabase.Element.Fire;
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
        Spell launchedspell = SpellDatabase.GetInstance().GetSpell(firstEle, secondEle);
        Debug.Log(launchedspell);
        float xAxis = Input.GetAxis(myInputManager.GetLookAxisHorizontal());
        float yAxis = Input.GetAxis(myInputManager.GetLookAxisVertical());
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
