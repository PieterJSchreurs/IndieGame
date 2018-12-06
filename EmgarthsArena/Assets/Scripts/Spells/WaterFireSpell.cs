using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFireSpell : Spell {

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 10;
        castTime = 0.5f;
        manaDrain = 5;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Light;
    }
    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterFireSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {
            
    }

    protected override void HandleCollision(Collision2D collision)
    {
        base.HandleCollision(collision);
        //Handle explosion effects.
        HandleExplosion();
        //Destroy the object.
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {

    }

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
        }
        return castTime;
    }

    public override float GetManaCost()
    {
        if (manaDrain == -1)
        {
            InitializeSpell();
        }
        return manaDrain;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
