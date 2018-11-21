using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {

    private Vector3[] respawnPoints;
    //Do we need variables to hold the boundaries?

	// Use this for initialization
	void Start () {
		//Populate the respawnPoints array here, just hard code the positions. (Or do something else if you know a better way.)
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3[] GetAllRespawnPoints()
    {
        return respawnPoints;
    }
    public Vector3 GetRespawnPoint(int id)
    {
        if (id >= respawnPoints.Length)
        {
            return respawnPoints[0]; //Could throw an error.
        }
        return respawnPoints[id];
    }
    public Vector3 GetRandomRespawnPoint()
    {
        return respawnPoints[Random.Range(0, respawnPoints.Length)];
    }

    private void handleCamera()
    {

    }

    private void handleBoundaries()
    {

    }
}
