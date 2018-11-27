using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWaterSpell : Spell {

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 0;
        castTime = 0.5f;
        manaDrain = 20;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.EarthWaterSpeed;
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
        return castTime;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
