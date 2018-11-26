using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPCamera : MonoBehaviour {

    public Transform _target;
    private Transform[] _targets;

	// Use this for initialization
	void Start () {

	}

    public void SetTargets(Player[] players)
    {
        _targets = new Transform[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            _targets[i] = players[i].transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (_targets == null)
        {
            return;
        }

        float minimumX = 100000;
        float maximumX = -100000;
        float minimumY = 100000;
        float maximumY = -100000;
        float largestDiff = 0;
        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].position.x < minimumX)
            {
                minimumX = _targets[i].position.x;
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }
            if (_targets[i].position.x > maximumX)
            {
                maximumX = _targets[i].position.x;
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }

            if (_targets[i].position.y < minimumY)
            {
                minimumY = _targets[i].position.y;
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
            if (_targets[i].position.y > maximumY)
            {
                maximumY = _targets[i].position.y;
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
        }

        float XPos = minimumX + ((maximumX - minimumX) / 2);
        float YPos = minimumY + ((maximumY - minimumY) / 2);
        Vector3 targetPos = new Vector3(XPos, YPos + Glob.camYOffset, 0);
        Vector3 targetDiff = targetPos - transform.position;
        transform.position = new Vector3(transform.position.x + (targetDiff.x * Glob.camSpeed), transform.position.y + (targetDiff.y * Glob.camSpeed), -10-(largestDiff/2));

        //float YDist = _target.position.y - transform.position.y + Glob.camYOffset;
        //transform.position = new Vector3(transform.position.x, transform.position.y + (YDist*Glob.camSpeed), transform.position.z);
	}
}
