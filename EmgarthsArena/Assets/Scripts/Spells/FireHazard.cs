using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : Spell {

    private const int fireRemainTime = 5;

    private bool _standingStill = false;
    private float _standingStillStartTime = 0;

    private void InitializeSpell()
    {
        base.Start();
        knockback = 0;
        damage = 5;
        castTime = 0f;
        manaDrain = 0;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Light;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireHazardSpeed;
        if (castTime == -1)
        {
            InitializeSpell();
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (_rb.velocity.magnitude <= 0.5f) //If the rock is stationary for 2 seconds
            {
                if (!_standingStill)
                {
                    _standingStill = true;
                    _standingStillStartTime = Time.time;
                }
                if (Time.time - _standingStillStartTime >= fireRemainTime)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                _standingStill = false;
            }
        }
    }

    protected override void HandleCollision(Collision2D collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HandleSpellHit(this, knockback, damage, _rb.velocity.normalized);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Spell>() == null)
        {
            if (collision.relativeVelocity.y >= 0)
            {
                _rb.velocity = Vector2.zero;
                _rb.isKinematic = true;
                _coll.isTrigger = true; //TODO: Enable OnTriggerEnter2D in the Spell script
            }
        }
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
        Move(false);
    }
}
