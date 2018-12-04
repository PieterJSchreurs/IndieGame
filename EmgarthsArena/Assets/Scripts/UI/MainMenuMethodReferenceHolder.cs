using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMethodReferenceHolder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void SwitchToGameScene()
    {
        SceneManager.GetInstance().SwitchScene(2);
        //SceneManager.GetInstance().InitializeMatch();
    }

    public void EndGame()
    {
        SceneManager.GetInstance().EndGame();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
