using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaterSpell : Spell
{
    private void InitializeSpell()
    {
        knockback = 50;
        damage = 10;
        castTime = 0.5f;
        manaDrain = 5;
        spellType = SpellDatabase.SpellType.Aoe;
        attackType = SpellDatabase.AttackType.Light;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireWaterSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {
        //Handle explosion effects.
        HandleExplosion();
        
        //Destroy the object.
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        //Adding effect + rotating it the right way.
        GameObject explosion = Instantiate(Glob.GetExplosionPrefab(), this.gameObject.transform.position, this.gameObject.transform.rotation);
        explosion.transform.eulerAngles = new Vector3(0, 90, 90);
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
    void Update()
    {

    }
}
