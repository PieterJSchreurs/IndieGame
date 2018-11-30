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
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HandleSpellHit(this, knockback, damage, -collision.relativeVelocity.normalized);
        }
    }

    public int[] GetKnockBackAndDamage()
    {
        int[] values = new int[2] { knockback, damage };
        return values;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Waterwater spell got hit.
        if (this.gameObject.GetComponent<WaterWaterSpell>() != null)
        {
            WaterWaterSpell waterWaterSpell = this as WaterWaterSpell;
            //Collided with player
            if(col.gameObject.GetComponent<Player>() != null && waterWaterSpell.GetPlayerCaster() != col.gameObject.GetComponent<Player>())
            {
                Debug.Log(col.gameObject);
                Player player = col.gameObject.GetComponent<Player>();
                player.HandleSpellHit(this, knockback, damage, -(this.transform.position - col.gameObject.transform.position).normalized);
            }
            //Collided with spell
            else if(col.gameObject.GetComponent<Spell>() != null)
            {
                Destroy(col.gameObject);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.isTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (this.GetComponent<FireEarthSpell>() == null)
                {
                    Player player = collision.gameObject.GetComponent<Player>();
                    Debug.Log("Oncollisionenter");
                    player.HandleSpellHit(this, knockback, damage, -collision.relativeVelocity.normalized);
                }
            }

            HandleCollision(collision);
        }
    }

    public abstract float GetCastTime();
}
