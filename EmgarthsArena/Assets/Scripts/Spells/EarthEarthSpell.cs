using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarthSpell : Spell {

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 20;
        castTime = 2;
        manaDrain = 70;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Heavy;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.EarthEarthSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {

    }

    protected override void HandleExplosion()
    {

    }

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
            Debug.Log("Initializing spell");
        }
        return castTime;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
