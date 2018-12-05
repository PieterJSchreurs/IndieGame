using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

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

    private GameObject backgroundUI;
    private GameObject resolutionScreen;

    private static int currentScene;
    private static int currentArenaIndex = 1;
    private static Arena currentArena;
    private static Player[] allPlayers;
    private int playersAlive;
    private bool initializingMatch = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Glob.FillInputDictionary();

        if (_instance == null)
        {
            _instance = this;
        }
        SwitchScene(1);
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update() {
        
    }
   
    public void SwitchScene(int id)
    {
        SoundManager.GetInstance().StopBackGroundMusic();
        Destroy(this.gameObject.GetComponent<FMODUnity.StudioListener>());
        Debug.Log("SWITCHING SCENE!");
        currentScene = id;
        Application.LoadLevel(id);
    }

    public void StartGameOnLevel(int level)
    {
        currentArenaIndex = level;
        if (currentArenaIndex == 1)
        {
            currentArenaIndex = 0; //TODO: Remove this if statement once the actual Arena2 prefab is in the game. -------------------------------------
        }
        SwitchScene(2);
    }

    public Arena GetCurrentArena()
    {
        return currentArena;
    }

    public void Rematch()
    {
        SwitchScene(2);
    }

    public void InitializeMatch()
    {
        if (currentScene != 2)
        {
            SwitchScene(2);
            return;
        }
        else if (backgroundUI == null)
        {
            backgroundUI = GameObject.FindGameObjectWithTag("UIParent");
            resolutionScreen = backgroundUI.transform.Find("ResolutionScreen").gameObject;
            //resolutionScreen = GameObject.FindGameObjectWithTag("ResolutionScreen");
            backgroundUI.SetActive(false);
            //resolutionScreen.SetActive(false);
        }
        Arena newArena = Instantiate(Glob.GetArenaPrefab(currentArenaIndex), Vector3.zero, new Quaternion(0, 0, 0, 0));
        currentArena = newArena;
        allPlayers = new Player[Glob.GetPlayerCount()];
        playersAlive = allPlayers.Length;
        if (allPlayers.Length == 0)
        {
            Debug.Log("no players");
            allPlayers = new Player[1];
            GameObject newPlayer = Instantiate(Glob.GetPlayerPrefab(), currentArena.GetRandomRespawnPoint(), new Quaternion(0, 0, 0, 0));
            allPlayers[0] = newPlayer.AddComponent<Player>().GetPlayer(-1);
        }
        else
        {
            if (allPlayers.Length == 1 && Input.GetJoystickNames()[0] == "") //TODO: Throws an error if no controllers connected.
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
                    newPlayer.name = "Player" + i;
                    allPlayers[i] = newPlayer.AddComponent<Player>().GetPlayer(i);
                    //Give the players their correct ID, and their correct InputManager.
                }
            }
        }

        currentArena.SetCameraTargets(allPlayers);
        this.gameObject.AddComponent<FMODUnity.StudioListener>();
        SoundManager.GetInstance().StartBackgroundMusic();
        SoundManager.GetInstance().SetBackGroundMusicIntensity(0.45f);
        //Setting to game.

        //Load an arena.
        //Use the Player constructor to place players at spawn points in the arena. (Don't forget to give them an InputManager.)
    }

    public void PlayerDown()
    {
        playersAlive--;
        if (playersAlive <= 1)
        {
            FinalizeMatch();
        }
    }

    public void FinalizeMatch() //TODO: Fix this mess.
    {
        currentArena.gameObject.SetActive(false);
        for (int i = 0; i < allPlayers.Length; i++)
        {
            allPlayers[i].gameObject.SetActive(false);
        }

        backgroundUI.SetActive(true);
        backgroundUI = null;
        resolutionScreen.SetActive(true);
        Image title = resolutionScreen.transform.Find("Title").GetComponent<Image>();

        GameObject playerStatsHolder = GameObject.FindGameObjectWithTag("ResolutionScreen");
        GameObject[] playerStats = new GameObject[Glob.GetPlayerCount()];
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (allPlayers[i].GetStats().lives > 0)
            {
                playerStats[i] = Instantiate(Resources.Load<GameObject>(Glob.PlayerWinBase + (i + 1).ToString()), playerStatsHolder.transform);
                title.sprite = Resources.Load<Sprite>(Glob.PlayerWinTitleBase + (i + 1).ToString());
            }
            else
            {
                playerStats[i] = Instantiate(Resources.Load<GameObject>(Glob.PlayerLoseBase + (i + 1).ToString()), playerStatsHolder.transform);
            }
            playerStats[i].transform.Find("Kills").GetComponentInChildren<Text>().text = allPlayers[i].GetStats().kills.ToString();
            playerStats[i].transform.Find("Deaths").GetComponentInChildren<Text>().text = (Glob.maxLives - allPlayers[i].GetStats().lives).ToString();
            playerStats[i].transform.Find("DmgDealt").GetComponentInChildren<Text>().text = allPlayers[i].GetStats().damageDealt.ToString();
            playerStats[i].transform.Find("DmgTaken").GetComponentInChildren<Text>().text = allPlayers[i].GetStats().damageTaken.ToString();
            //If: Player has lives remaining, instantiate a 'Winner' banner.
            //All other players get a 'Loser' banner

        }
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
