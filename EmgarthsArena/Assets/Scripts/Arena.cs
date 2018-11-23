using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena : MonoBehaviour {

    private Camera _myCamera;
    private Transform[] _cameraTargets;

    private struct playerInfoBanner
    {
        public GameObject banner;
        public Image firstElementIcon;
        public Image secondElementIcon;
    }
    private playerInfoBanner[] playerBanners;
    private GameObject characterInfoParent;
    [SerializeField]
    private Vector3[] respawnPoints;
    //Do we need variables to hold the boundaries?

    // Use this for initialization
    void Start () {
        _myCamera = GetComponentInChildren<Camera>();

        characterInfoParent = GameObject.FindGameObjectWithTag("CharacterInfo");
        playerBanners = new playerInfoBanner[Glob.GetPlayerCount()];
        for (int i = 0; i < playerBanners.Length; i++)
        {
            playerBanners[i] = new playerInfoBanner();
            GameObject newBanner = Instantiate(Resources.Load<GameObject>(Glob.PlayerBannerPrefab), characterInfoParent.transform);
            playerBanners[i].banner = newBanner;
            playerBanners[i].firstElementIcon = newBanner.transform.Find("Element1").GetComponent<Image>();
            playerBanners[i].secondElementIcon = newBanner.transform.Find("Element2").GetComponent<Image>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        handleCamera();
        handleBoundaries();
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

    public void UpdatePlayerBanner(int id, SpellDatabase.Element firstEle, SpellDatabase.Element secondEle)
    {
        switch (firstEle)
        {
            case SpellDatabase.Element.Fire:
                playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementIcon);
                break;
            case SpellDatabase.Element.Water:
                playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementIcon);
                break;
            case SpellDatabase.Element.Earth:
                playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementIcon);
                break;
            case SpellDatabase.Element.Null:
                break;
            default:
                break;
        }
        switch (secondEle)
        {
            case SpellDatabase.Element.Fire:
                playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementIcon);
                break;
            case SpellDatabase.Element.Water:
                playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementIcon);
                break;
            case SpellDatabase.Element.Earth:
                playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementIcon);
                break;
            case SpellDatabase.Element.Null:
                break;
            default:
                break;
        }
    }

    public void SetCameraTargets(Player[] players)
    {
        _cameraTargets = new Transform[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            _cameraTargets[i] = players[i].transform;
        }
    }

    private void handleCamera()
    {
        if (_cameraTargets == null)
        {
            return;
        }

        float minimumX = 100000;
        float maximumX = -100000;
        float minimumY = 100000;
        float maximumY = -100000;
        float largestDiff = 0;
        for (int i = 0; i < _cameraTargets.Length; i++)
        {
            if (_cameraTargets[i].position.x < minimumX)
            {
                minimumX = _cameraTargets[i].position.x;
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }
            if (_cameraTargets[i].position.x > maximumX)
            {
                maximumX = _cameraTargets[i].position.x;
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }

            if (_cameraTargets[i].position.y < minimumY)
            {
                minimumY = _cameraTargets[i].position.y;
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
            if (_cameraTargets[i].position.y > maximumY)
            {
                maximumY = _cameraTargets[i].position.y;
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
        }

        float XPos = minimumX + ((maximumX - minimumX) / 2);
        float YPos = minimumY + ((maximumY - minimumY) / 2);
        Vector3 targetPos = new Vector3(XPos, YPos + Glob.camYOffset, 0);
        Vector3 targetDiff = targetPos - _myCamera.transform.position;
        _myCamera.transform.position = new Vector3(_myCamera.transform.position.x + (targetDiff.x * Glob.camSpeed), _myCamera.transform.position.y + (targetDiff.y * Glob.camSpeed), -10 - (largestDiff / 2));
    }

    private void handleBoundaries()
    {

    }
}
