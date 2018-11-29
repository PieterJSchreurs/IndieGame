using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Spell {

    private const int rockRemainTime = 5;
    private const float rockLockTime = 0.75f;

    private float _lastVelocityX = 0;
    private float _lastVelocityY = 0;
    private bool _standingStill = false;
    private float _standingStillStartTime = 0;

    private void InitializeSpell()
    {
        base.Start();
        //_rb.velocity = -_rb.transform.up * Glob.EarthEarthSpeed;
        _lastVelocityX = _rb.velocity.x;
        knockback = 50;
        damage = 20;
        castTime = 0f;
        manaDrain = 0;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Heavy;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        if (castTime == -1)
        {
            InitializeSpell();
            Debug.Log("Initializing spell");
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (Mathf.Abs(_lastVelocityX) - Mathf.Abs(_rb.velocity.x) > 10f) //If the ice ball is brought to a sudden stop horizontally.
            {
                Destroy(gameObject);
            }
            if (_rb.velocity.magnitude <= 1.5f) //If the rock is stationary for 2 seconds
            {
                if (!_standingStill)
                {
                    _standingStill = true;
                    _standingStillStartTime = Time.time;
                }
                if (Time.time - _standingStillStartTime >= rockLockTime)
                {
                    //damage = 0;
                    //knockback = 0;
                }
                if (Time.time - _standingStillStartTime >= rockRemainTime)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                _standingStill = false;
            }
            _lastVelocityX = _rb.velocity.x;
            _lastVelocityY = _rb.velocity.y;
        }
    }

    protected override void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Debug.Log(_rb.velocity.magnitude + " ROCK");
            if (!_standingStill)
            {
                
                if (Vector3.Dot(collision.relativeVelocity, _rb.velocity) < 0)
                {
                    player.HandleSpellHit(this, knockback, Mathf.Min(damage, Mathf.RoundToInt(_rb.velocity.magnitude * 10)), Mathf.Min(1, _rb.velocity.magnitude) * collision.relativeVelocity.normalized);
                    _rb.velocity = Vector2.zero;
                }
            }
        }
        //base.HandleCollision(collision);
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
    void Update()
    {
        Move(false);
    }
}
