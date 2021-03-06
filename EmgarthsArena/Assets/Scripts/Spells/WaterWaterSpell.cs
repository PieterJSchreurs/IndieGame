﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaterSpell : Spell {

    private Player playerCaster;

    private void InitializeSpell()
    {
        knockback = 60;
        damage = 30;
        castTime = 0.2f;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }

    // Use this for initialization
    void Start () {
        SceneManager.GetInstance().GetCurrentArena().SetScreenShake(0.3f, 0.5f);
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterWaterSpeed;
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        this.gameObject.transform.localPosition -= new Vector3(0, 1, 0);
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
        Destroy(this.gameObject, Glob.WaterwaterAliveTime);
	}
}
