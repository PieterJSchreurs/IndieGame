using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWaterSpell : Spell
{


    // Use this for initialization
    void Start()
    {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireWaterSpeed;

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
        GameObject explosion = Instantiate(Glob.GetExplosionPrefab(), this.gameObject.transform.position, this.gameObject.transform.rotation);
        explosion.transform.eulerAngles = new Vector3(0, 90, 90); 

        Debug.Log("Explode");
    }


    // Update is called once per frame
    void Update()
    {

    }
}
