using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEarthSpell : Spell {

    private float _lastVelocityX = 0;
    private float _lastVelocityY = 0;
    private bool _standingStill = false;
    private float _standingStillStartTime = 0;

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 40;
        castTime = 1.5f;
        manaDrain = 25;
        _rb.gravityScale = 3;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterEarthSpeed;
        _lastVelocityX = _rb.velocity.x;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (Mathf.Abs(_lastVelocityX) - Mathf.Abs(_rb.velocity.x) > 10f) //If the ice ball is brought to a sudden stop horizontally.
            {
                Destroy(gameObject);
            }
            if (Mathf.Abs(_rb.velocity.x) < 0.3f && Mathf.Abs(_rb.velocity.y) < 0.3f) //If the ice ball is stationary for 2 seconds
            {
                if (!_standingStill)
                {
                    _standingStill = true;
                    _standingStillStartTime = Time.time;
                }
                if (Time.time - _standingStillStartTime >= 2)
                {
                    Destroy(gameObject);
                }
                //Destroy(gameObject);
            } else
            {
                _standingStill = false;
            }
            _lastVelocityX = _rb.velocity.x;
            _lastVelocityY = _rb.velocity.y;
        }
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
        Move(false);
	}
}
