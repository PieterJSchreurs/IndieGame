using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaterSpell : Spell {

    private Player playerCaster;
    private float AliveTime;

    private void InitializeSpell()
    {
        knockback = 60;
        damage = 30;
        castTime = 0.2f;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
        AliveTime = Time.time;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterWaterSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision(Collision2D collision)
    {
        //If it's not the player who casted it, don't do anything.
        if (collision.gameObject.GetComponent<Player>() != playerCaster)
        {
            base.HandleCollision(collision);
        } else
        {

        }
    }

    protected override void HandleExplosion()
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

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
        }
        return castTime;
    }

    // Update is called once per frame
    void Update () {
		if(Time.time >= AliveTime + Glob.WaterwaterAliveTime)
        {
            Destroy(this.gameObject);
        }
	}
}
