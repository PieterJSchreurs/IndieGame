using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMethodReferenceHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void CallInitializeGame()
    {
        SceneManager.GetInstance().InitializeMatch();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
