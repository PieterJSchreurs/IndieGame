using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEarthSpell : Spell {


    private void InitializeSpell()
    {
        knockback = 50;
        damage = 40;
        castTime = 1.5f;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterEarthSpeed;
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
