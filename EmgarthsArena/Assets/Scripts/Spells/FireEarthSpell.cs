using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEarthSpell : Spell
{

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 40;
        castTime = 1;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }
    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireEarthSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {
        //Handle explosion effects.
        HandleExplosion();
        //Spawn explosion thing which pushes things away.
        //Destroy the object.
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        //GameObject knockback = Glob.GetKnockback();
        //knockback = Instantiate(knockback, this.gameObject.transform);
        //knockback.transform.parent = this.gameObject.transform.parent;
        //CircleCollider2D circleCollider = knockback.GetComponent<CircleCollider2D>();
        //circleCollider.enabled = false;
        //circleCollider.enabled = true;

       // Collider2D[] listOfColliders = circleCollider.OverlapCollider();
        
    }

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
            castTime = 1;
            Debug.Log("Initializing spell");
        }
        return castTime;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
