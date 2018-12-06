using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaterSpell : Spell
{
    private Player playerCaster;

    private void InitializeSpell()
    {
        knockback = 0;
        damage = 3;
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

    public void SetPlayerCaster(Player pPlayer)
    {
        playerCaster = pPlayer;
    }

    public Player GetPlayerCaster()
    {
        return playerCaster;
    }

    protected override void HandleCollision(Collision2D collision)
    {
        SoundManager.GetInstance().PlaySound(Glob.WaterballHitSound);
        if (collision.gameObject.GetComponent<Player>() != playerCaster)
        {
            base.HandleCollision(collision);
        }
        else
        {

        }
    }

    protected override void HandleExplosion()
    {
        //
        //Adding effect + rotating it the right way.
        //GameObject explosion = Instantiate(Glob.GetFogPrefab(), this.gameObject.transform.position, this.gameObject.transform.rotation);
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
        Destroy(this.gameObject, Glob.FireWaterAliveTime);
    }
}
