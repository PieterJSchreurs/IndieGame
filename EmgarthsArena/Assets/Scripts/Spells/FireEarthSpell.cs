using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEarthSpell : Spell
{

    private void InitializeSpell()
    {
        knockback = 35;
        damage = 25;
        castTime = 0.5f;
        manaDrain = 15;
        spellType = SpellDatabase.SpellType.Projectile;
        attackType = SpellDatabase.AttackType.Medium;
    }
    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireEarthSpeed;
        InitializeSpell();
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision(Collision2D collision)
    {
        //base.HandleCollision(collision);
        //Handle explosion effects.
        HandleExplosion();
        //Spawn explosion thing which pushes things away.
        //Destroy the object.
        SceneManager.GetInstance().RemoveMovingObject(this);
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        SoundManager.GetInstance().PlaySound(Glob.MeteorExplosionSound);
        SceneManager.GetInstance().GetCurrentArena().SetScreenShake(0.3f, 0.5f);

        GameObject knockBackGameObject = Glob.GetKnockback();
        knockBackGameObject = Instantiate(knockBackGameObject, this.gameObject.transform);
        knockBackGameObject.transform.localPosition = new Vector3(0, 0, 0);
        knockBackGameObject.transform.parent = this.gameObject.transform.parent;
        knockBackGameObject.transform.localScale += new Vector3(3, 3, 3);
        CircleCollider2D circleCollider = knockBackGameObject.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        circleCollider.enabled = true;

        ContactFilter2D contactFilter2D = new ContactFilter2D();
        Collider2D[] intersectingresults = new Collider2D[9];
        circleCollider.OverlapCollider(contactFilter2D, intersectingresults);

        int i = 0;
        foreach (Collider2D col in intersectingresults)
        {
            if (col != null)
            {
                Debug.Log(i + " " + col.gameObject.name);
                i++;
                if (col.gameObject.tag == "Player")
                {
                    Player player = col.gameObject.GetComponent<Player>();
                    Vector2 hitAngle = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
                    player.HandleSpellHit(this, knockback, damage, hitAngle.normalized);
                }
            }
        }
        //Destroy this instantly if necessarily, probably keep it alive for explosion animation.
        Destroy(knockBackGameObject.gameObject, 1.25f);
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

    }
}
