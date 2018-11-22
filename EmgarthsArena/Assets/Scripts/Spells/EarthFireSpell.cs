using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthFireSpell : Spell {

	// Use this for initialization
	void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.EarthFireSpeed;
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {

    }

    protected override void HandleExplosion()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
