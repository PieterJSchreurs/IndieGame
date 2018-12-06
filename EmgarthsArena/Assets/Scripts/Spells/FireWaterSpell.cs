using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaterSpell : Spell
{
    private Player playerCaster;
    private List<Player> damagingPlayers = new List<Player>();

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

    public void AddPlayerInSteam(Player plyr)
    {
        if (!damagingPlayers.Contains(plyr))
        {
            damagingPlayers.Add(plyr);
        }
    }

    protected override void HandleCollision(Collision2D collision)
    {

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

    void OnDestroy()
    {
        for (int i = 0; i < damagingPlayers.Count; i++)
        {
            damagingPlayers[i].SetSteamCloud(damage, false);
        }
        //ContactFilter2D contactFilter2D = new ContactFilter2D();
        //Collider2D[] intersectingresults = new Collider2D[9];
        //_coll.OverlapCollider(contactFilter2D, intersectingresults);

        //foreach (Collider2D col in intersectingresults)
        //{
        //    if (col != null)
        //    {
        //        Debug.Log(col.name);
        //        if (col.gameObject.GetComponent<Player>() != null)
        //        {
        //            col.gameObject.GetComponent<Player>().SetSteamCloud(damage, false);
        //        }
        //    }
        //}

        SceneManager.GetInstance().RemoveMovingObject(this);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, Glob.FireWaterAliveTime); //TODO: Toggle all players to be out of the steam cloud (currently continues damaging infinitely)
    }
}
