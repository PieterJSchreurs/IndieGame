using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPCamera : MonoBehaviour {

    public Transform _target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //float YDist = _target.position.y - transform.position.y + Glob.camYOffset;
        //transform.position = new Vector3(transform.position.x, transform.position.y + (YDist*Glob.camSpeed), transform.position.z);
	}
}
