using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFireSpell : Spell {

    private const int fireHazardCount = 5;

    private void InitializeSpell()
    {
        knockback = 25;
        damage = 30;
        castTime = 0.5f;
        manaDrain = 20;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Medium;
    }
    // Use this for initialization
    void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.EarthFireSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision(Collision2D collision)
    {
        base.HandleCollision(collision);
        //Handle explosion effects.
        HandleExplosion();

        //Destroy the object.
        SceneManager.GetInstance().RemoveMovingObject(this);
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        SoundManager.GetInstance().PlaySound(Glob.FirerockImpactSound);
        //Adding effect + rotating it the right way.
        int rotationOffset = Random.Range(-45, 45);
        for (int i = 0; i < fireHazardCount; i++)
        {
            GameObject fireHazard = Instantiate(Resources.Load<GameObject>(Glob.FireHazardPrefab), transform.position, new Quaternion());
            fireHazard.GetComponent<Spell>().SetPlayer(myPlayer);
            fireHazard.transform.eulerAngles = new Vector3(0, 0, -180 + (360f * ((float)i/fireHazardCount)) + rotationOffset);
        }
        SceneManager.GetInstance().GetCurrentArena().SetScreenShake(0.3f, 0.5f);
        //GameObject explosion = Instantiate(Glob.GetExplosionPrefab(), this.gameObject.transform.position, this.gameObject.transform.rotation);
        //explosion.transform.eulerAngles = new Vector3(0, 90, 90);
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
		
	}
}
