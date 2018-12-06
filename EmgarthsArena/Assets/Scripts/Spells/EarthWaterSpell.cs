using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthWaterSpell : Spell {

    private float nextAction = 0.0f;

    private void InitializeSpell()
    {
        knockback = 0;
        damage = 0;
        castTime = 0.10f;
        manaDrain = 20;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, -Vector3.up, 5f, 1 << 9);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * _coll.bounds.extents.x, -Vector3.up, 5f, 1 << 9);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position - Vector3.right * _coll.bounds.extents.x, -Vector3.up, 5f, 1 << 9);
        if (hitCenter != null)
        {
            transform.position = new Vector3(transform.position.x, hitCenter.point.y, transform.position.z);
        }
        else if (hitRight != null)
        {
            transform.position = new Vector3(transform.position.x, hitRight.point.y, transform.position.z);
        }
        else if (hitLeft != null) {
            transform.position = new Vector3(transform.position.x, hitLeft.point.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
        _rb.velocity = -_rb.transform.up * Glob.EarthWaterSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {
        if(isFixed)
        {
            Destroy(this.gameObject, Glob.EarthWaterAliveTime);
            if(Time.time > nextAction && this.gameObject.transform.localScale.y < Glob.maxPilarHeight)
            {
                nextAction += 0.1f;
                this.gameObject.transform.localScale += new Vector3(0, 0.1f, 0);
            }
        }
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
            Move(true);
        }
	}
}
