using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {
    private static SceneManager _instance;
    public static SceneManager GetInstance()
    {
        if (_instance == null)
        {
            Debug.Log("There is no instance yet, this is not supposed to happen.");
            _instance = new SceneManager();
        }
        return _instance;
    }

    private SceneManager()
    {
        //This initializer is called alot, not sure why, be careful when using it.
    }

    private static int currentScene;
    private static Arena currentArena;
    private static Player[] allPlayers;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Glob.FillInputDictionary();

        if (_instance == null)
        {
            _instance = this;
        }
        Application.LoadLevel(1);

    }

    // Use this for initialization
    void Start () {
        InitializeMatch();
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
        Arena newArena = Instantiate(Glob.GetArenaPrefab(0), Vector3.zero, new Quaternion(0, 0, 0, 0));
        currentArena = newArena;
        allPlayers = new Player[Glob.GetPlayerCount()];
        if (allPlayers.Length == 0)
        {
            Debug.Log("no players");
            allPlayers = new Player[1];
            GameObject newPlayer = Instantiate(Glob.GetPlayerPrefab(), currentArena.GetRandomRespawnPoint(), new Quaternion(0, 0, 0, 0));
            allPlayers[0] = newPlayer.AddComponent<Player>().GetPlayer(-1);
        }
        else
        {
            Debug.Log(Input.GetJoystickNames().Length);
            if (allPlayers.Length == 1 && Input.GetJoystickNames()[0] == "")
            {
                Debug.Log("no players2");
                allPlayers = new Player[1];
                GameObject newPlayer = Instantiate(Glob.GetPlayerPrefab(), currentArena.GetRandomRespawnPoint(), new Quaternion(0, 0, 0, 0));
                allPlayers[0] = newPlayer.AddComponent<Player>().GetPlayer(-1);
            }
            else
            {
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    GameObject newPlayer = Instantiate(Glob.GetPlayerPrefab(), currentArena.GetRandomRespawnPoint(), new Quaternion(0, 0, 0, 0));
                    allPlayers[i] = newPlayer.AddComponent<Player>().GetPlayer(i);
                    //Give the players their correct ID, and their correct InputManager.
                }
            }
        }

        currentArena.SetCameraTargets(allPlayers);

        //Load an arena.
        //Use the Player constructor to place players at spawn points in the arena. (Don't forget to give them an InputManager.)
    }

    public void FinalizeMatch()
    {

    }
}
