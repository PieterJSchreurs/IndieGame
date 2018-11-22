using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFireSpell : Spell {

    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.WaterFireSpeed;

    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {
        //Handle explosion effects.
        HandleExplosion();
        //Destroy the object.
        Destroy(this.gameObject);
    }

    protected override void HandleExplosion()
    {
        Debug.Log("Explode");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
