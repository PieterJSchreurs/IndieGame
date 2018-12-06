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

    public void CallRematch()
    {
        SceneManager.GetInstance().Rematch();
    }

    public void BackToMain()
    {
        SceneManager.GetInstance().SwitchScene(1);
    }

    public void BackToLevelSelect()
    {
        SceneManager.GetInstance().SwitchScene(1);
    }

    public void TogglePause(bool pToggle)
    {
        SceneManager.GetInstance().TogglePauseGame(pToggle);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
