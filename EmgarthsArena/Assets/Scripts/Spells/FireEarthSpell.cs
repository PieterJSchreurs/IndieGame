using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEarthSpell : Spell
{

    private void InitializeSpell()
    {
        knockback = 20;
        damage = 25;
        castTime = 1;
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

    protected override void HandleCollision()
    {
        //Handle explosion effects.
        HandleExplosion();
        //Spawn explosion thing which pushes things away.
        //Destroy the object.
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        Debug.Log("Exploooooosion");
        GameObject knockBackGameObject = Glob.GetKnockback();
        knockBackGameObject = Instantiate(knockBackGameObject, this.gameObject.transform);
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

        Destroy(knockBackGameObject.gameObject, 2.0f);
    }

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
            castTime = 1;
            Debug.Log("Initializing spell");
        }
        return castTime;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
