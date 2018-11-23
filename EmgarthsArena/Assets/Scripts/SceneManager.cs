using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    private static SceneManager _instance;
    public static SceneManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SceneManager();
        }
        return _instance;
    }

    private SceneManager()
    {
        //Initialize menu's etc here.
      
    }

    private int currentScene;
    private Arena currentArena;
    private Player[] allPlayers;


    // Use this for initialization
    void Start () {
        Glob.FillInputDictionary();
        //Swap this out for Input.GetJoystickNames()
        for (int i = 0; i < 2; i++)
        {
            Player player = Instantiate(Glob.GetPlayerPrefab(), Glob.spawningPoints[i], Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchScene(int id)
    {

    }

    public Arena GetCurrentArena()
    {
        return currentArena;
    }

    public void InitializeMatch()
    {
        //Load an arena.
        //Use the Player constructor to place players at spawn points in the arena. (Don't forget to give them an InputManager.)
    }

    public void FinalizeMatch()
    {

    }
}
