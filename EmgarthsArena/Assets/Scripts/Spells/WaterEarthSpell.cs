using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEarthSpell : Spell {

    private const int snowballRemainTime = 5;

    private float _lastVelocityX = 0;
    private float _lastVelocityY = 0;
    private bool _standingStill = false;
    private float _standingStillStartTime = 0;

    private void InitializeSpell()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterEarthSpeed;
        _lastVelocityX = _rb.velocity.x;
        knockback = 50;
        damage = 40;
        castTime = 0.75f;
        manaDrain = 25;
        //_rb.gravityScale = 3;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        if (castTime == -1)
        {
            InitializeSpell();
        }
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            if (Mathf.Abs(_lastVelocityX) - Mathf.Abs(_rb.velocity.x) > 10f) //If the ice ball is brought to a sudden stop horizontally.
            {
                SceneManager.GetInstance().RemoveMovingObject(this);
                Destroy(gameObject);
            }
            if (Mathf.Abs(_rb.velocity.x) < 0.3f && Mathf.Abs(_rb.velocity.y) < 0.3f) //If the ice ball is stationary for 2 seconds
            {
                if (!_standingStill)
                {
                    _standingStill = true;
                    _standingStillStartTime = Time.time;
                }
                if (Time.time - _standingStillStartTime >= snowballRemainTime)
                {
                    SceneManager.GetInstance().RemoveMovingObject(this);
                    Destroy(gameObject);
                }
            } else
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
            if (!_standingStill)
            {
                if (Vector3.Dot(collision.relativeVelocity, _rb.velocity) <= 0)//TODO: If a player is running away from a snowball/rock, it wont deal damage.
                {
                    Vector3 velo = _rb.velocity;
                    _rb.velocity = Vector2.zero;
                    player.HandleSpellHit(this, knockback, Mathf.Min(damage, Mathf.RoundToInt(velo.magnitude * 10)), Mathf.Min(1, velo.magnitude) * new Vector2(Mathf.Sign(velo.normalized.x), 0));
                    SceneManager.GetInstance().GetCurrentArena().SetScreenShake(0.3f, 0.5f);
                }
            }
        }
        else if (collision.gameObject.tag == "Ground")
        {
            SceneManager.GetInstance().GetCurrentArena().SetScreenShake(0.2f, 0.3f);
        }
        //base.HandleCollision(collision);
        //Destroy the object.
        //Destroy(this.gameObject);
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
    void Update () {
        if (!_isPaused)
        {
            Move(false);
        }
	}
}
