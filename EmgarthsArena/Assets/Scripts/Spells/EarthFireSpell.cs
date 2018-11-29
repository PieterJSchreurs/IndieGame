using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFireSpell : Spell {

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 30;
        castTime = 0.5f;
        manaDrain = 30;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Medium;
    }
    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.EarthFireSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision(Collision2D collision)
    {
        base.HandleCollision(collision);
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
