using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MovingObject
{
    protected Player myPlayer;
    protected int damage;
    protected int knockback;
    protected float castTime = -1;
    protected float manaDrain = -1;
    protected abstract void HandleExplosion();
    protected SpellDatabase.Element firstElement;
    protected SpellDatabase.Element secondElement;
    protected SpellDatabase.SpellType spellType;
    protected SpellDatabase.AttackType attackType;

    protected override void HandleCollision(Collision2D collision)
    {

    }

    public void SetPlayer(Player plyr)
    {
        myPlayer = plyr;
    }
    public Player GetPlayer()
    {
        return myPlayer;
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
            if (col.gameObject.GetComponent<Player>() != null && waterWaterSpell.GetPlayerCaster() != col.gameObject.GetComponent<Player>())
            {
                Player player = col.gameObject.GetComponent<Player>();
                player.HandleSpellHit(this, knockback, damage, -(this.transform.position - col.gameObject.transform.position).normalized);
            }
            //Collided with spell
            else if (col.gameObject.GetComponent<Spell>() != null)
            {
                Debug.Log("Waterwater collided with spell");
                Destroy(col.gameObject);
            }
        }
        if (this.gameObject.GetComponent<FireWaterSpell>() != null)
        {
            FireWaterSpell fireWaterSpell = this as FireWaterSpell;
            //Collided with player
            if (col.gameObject.GetComponent<Player>() != null && fireWaterSpell.GetPlayerCaster() != col.gameObject.GetComponent<Player>())
            {
                Player player = col.gameObject.GetComponent<Player>();
                if(player != fireWaterSpell.GetPlayerCaster())
                {
                    player.SetSteamCloud(this.damage, true);
                    Debug.Log("Setting steamcloud");
                }
            }
            //Collided with spell
            else if (col.gameObject.GetComponent<Spell>() != null)
            {
                Destroy(col.gameObject);
            }
        }
        if (this.gameObject.GetComponent<FireHazard>() != null)
        {
            if (col.gameObject.tag == "Player")
            {
                Player player = col.gameObject.GetComponent<Player>();
                player.HandleSpellHit(this, knockback, damage, Vector2.zero);
                Destroy(gameObject);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.otherCollider.isTrigger)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("OnCollisionEnter with player");
                if (this.GetComponent<FireEarthSpell>() == null)
                {
                    Player player = collision.gameObject.GetComponent<Player>();
                    player.HandleSpellHit(this, knockback, damage, -collision.relativeVelocity.normalized);
                }
            }

            HandleCollision(collision);
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (GetComponent<FireWaterSpell>() != null && col.gameObject.tag == "Player")
        {
            Player player = col.gameObject.GetComponent<Player>();
            player.SetSteamCloud(damage, false);
        }
    }

    public abstract float GetCastTime();

    public abstract float GetManaCost();

}
