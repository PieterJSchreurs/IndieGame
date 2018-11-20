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

    public Player()
    {

    }

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update () {
		
	}

    protected override void Move()
    {
        throw new NotImplementedException();
    }

    protected override void HandleCollision()
    {
        throw new NotImplementedException();
    }

    private void jump()
    {

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
