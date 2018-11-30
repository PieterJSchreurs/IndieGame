using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MovingObject
{
    protected int damage;
    protected int knockback;
    protected float castTime = -1;
    protected float manaDrain;
    protected abstract void HandleExplosion();
    protected SpellDatabase.Element firstElement;
    protected SpellDatabase.Element secondElement;
    protected SpellDatabase.SpellType spellType;
    protected SpellDatabase.AttackType attackType;

    protected override void HandleCollision(Collision2D collision)
    {

    }

    public int[] GetKnockBackAndDamage()
    {
        int[] values = new int[2] { knockback, damage };
        return values;
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.isTrigger)
        {
            if (col.gameObject.tag == "Player")
            {
                Player player = col.gameObject.GetComponent<Player>();

                //Fire earth doesn't need a spellhit since it explodes on impact, the explosion is the thing that deals the hit.
                if (this.GetComponent<FireEarthSpell>() != null)
                {
                    Debug.Log("Fire earth spell hit.");
                }
                else
                {
                    player.HandleSpellHit(this, knockback, damage, this.gameObject.GetComponent<Rigidbody2D>().velocity.normalized);
                }
            }

            HandleCollision();
        }
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!collision.otherCollider.isTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (this.GetComponent<FireEarthSpell>() == null)
                {
                    Player player = collision.gameObject.GetComponent<Player>();

                    player.HandleSpellHit(this, knockback, damage, -collision.relativeVelocity.normalized);
                }
            }

            HandleCollision(collision);
        }
    }

    public abstract float GetCastTime();
}
