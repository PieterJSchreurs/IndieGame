using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Selectable>().Select();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
