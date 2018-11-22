using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireSpell : Spell {

    
	// Use this for initialization
	void Start () {
        base.Start();
        _rb.velocity = -_rb.transform.up * Glob.FireFireSpeed;
        
    }

    protected override void Move(bool isFixed)
    {

    }

    protected override void HandleCollision()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
