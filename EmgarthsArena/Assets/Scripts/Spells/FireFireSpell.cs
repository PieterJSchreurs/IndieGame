using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireSpell : Spell {


	// Use this for initialization
	void Start () {
        _rb.velocity = transform.forward * Glob.FireFireSpeed;
    }

    protected override void Move()
    {

    }

    protected override void HandleCollision()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
