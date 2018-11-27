using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfter : MonoBehaviour {

    public float DestroyDelay;
    private float spawnTime;

	// Use this for initialization
	void Start () {
        spawnTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - spawnTime >= DestroyDelay)
        {
            Destroy(gameObject);
        }
	}
}
