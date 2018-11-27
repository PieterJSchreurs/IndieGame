using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaterSpell : Spell {

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 30;
        castTime = 1;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterWaterSpeed;
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
