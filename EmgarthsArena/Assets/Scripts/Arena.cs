using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena : MonoBehaviour {

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
        //The respawnPoints array is populated through the inspector of the prefab.
        //Spawn player info banners for every player here. (amount can be passed from the scenemanager.)
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

    private void handleCamera()
    {

    }

    private void handleBoundaries()
    {

    }
}
