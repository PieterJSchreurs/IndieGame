using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Arena : MonoBehaviour {

    private Camera _myCamera;
    private Collider2D _myColl;
    private Transform[] _cameraTargets;

    private bool _shouldScreenShake = false;
    private float _shakeDistance;
    private float _shakeTime;
    private float _shakeStartTime;

    private struct playerInfoBanner
    {
        public GameObject banner;
        public Image healthBar;
        public Image manaBar;
        public Image[] lifeCrystals;
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
        _myColl = GetComponent<Collider2D>();
        if (playerBanners == null)
        {
            initializePlayerBanners();
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

    private void initializePlayerBanners()
    {
        characterInfoParent = GameObject.FindGameObjectWithTag("CharacterInfo");
        playerBanners = new playerInfoBanner[Glob.GetPlayerCount()];
        for (int i = 0; i < playerBanners.Length; i++)
        {
            playerBanners[i] = new playerInfoBanner();
            playerBanners[i].lifeCrystals = new Image[Glob.maxLives];
            GameObject newBanner = Instantiate(Resources.Load<GameObject>(Glob.PlayerBannerPrefab), characterInfoParent.transform);
            playerBanners[i].banner = newBanner;
            playerBanners[i].healthBar = newBanner.transform.Find("HealthBar").GetComponent<Image>();
            playerBanners[i].manaBar = newBanner.transform.Find("ManaBar").GetComponent<Image>();
            playerBanners[i].firstElementIcon = newBanner.transform.Find("Glow1").Find("Element").GetComponent<Image>();
            playerBanners[i].secondElementIcon = newBanner.transform.Find("Glow2").Find("Element").GetComponent<Image>();
            for (int j = 0; j < playerBanners[i].lifeCrystals.Length; j++)
            {
                playerBanners[i].lifeCrystals[j] = newBanner.transform.Find("HealthCrystal" + (j + 1).ToString()).GetComponent<Image>();
            }
        }
    }

    public void UpdatePlayerBanner(int id, SpellDatabase.Element firstEle, SpellDatabase.Element secondEle, int health, float mana, int lives)
    {
        if (playerBanners == null)
        {
            initializePlayerBanners();
        }

        playerBanners[id].healthBar.fillAmount = (float)health / Glob.maxHealth;
        playerBanners[id].manaBar.fillAmount = (float)mana / Glob.maxMana;
        for (int i = 0; i < playerBanners[id].lifeCrystals.Length; i++)
        {
            if (playerBanners[id].lifeCrystals.Length - i <= lives)
            {
                playerBanners[id].lifeCrystals[i].sprite = Resources.Load<Sprite>(Glob.FullLifeCrystalBase + (id + 1).ToString());
            } else
            {
                playerBanners[id].lifeCrystals[i].sprite = Resources.Load<Sprite>(Glob.EmptyLifeCrystal);
            }
        }

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

    public void GlowPlayerBannerElement(int id, bool firstOrSecondElement, SpellDatabase.Element element, bool toggle)
    {
        if (toggle)
        {
            if (!firstOrSecondElement)
            {
                switch (element)
                {
                    case SpellDatabase.Element.Fire:
                        playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Water:
                        playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Earth:
                        playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Null:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (element)
                {
                    case SpellDatabase.Element.Fire:
                        playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Water:
                        playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Earth:
                        playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                        break;
                    case SpellDatabase.Element.Null:
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if (!firstOrSecondElement)
            {
                switch (element)
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
            }
            else
            {
                switch (element)
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
        }
    }

    public void GlowBackgroundPlayerBannerElement(int id, bool firstOrSecondElement, SpellDatabase.Element element, bool toggle)
    {
        if (toggle)
        {
            if (!firstOrSecondElement)
            {
                playerBanners[id].firstElementIcon.transform.parent.GetComponent<Image>().enabled = true;
                switch (element)
                {
                    case SpellDatabase.Element.Fire:
                        //playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                        playerBanners[id].firstElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.FireBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Water:
                        //playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                        playerBanners[id].firstElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.WaterBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Earth:
                        //playerBanners[id].firstElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                        playerBanners[id].firstElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.EarthBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Null:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                playerBanners[id].secondElementIcon.transform.parent.GetComponent<Image>().enabled = true;
                switch (element)
                {
                    case SpellDatabase.Element.Fire:
                        //playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.FireElementSelectedIcon);
                        playerBanners[id].secondElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.FireBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Water:
                        //playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.WaterElementSelectedIcon);
                        playerBanners[id].secondElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.WaterBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Earth:
                        //playerBanners[id].secondElementIcon.sprite = Resources.Load<Sprite>(Glob.EarthElementSelectedIcon);
                        playerBanners[id].secondElementIcon.transform.parent.GetComponent<Image>().sprite = Resources.Load<Sprite>(Glob.EarthBackgroundGlow);
                        break;
                    case SpellDatabase.Element.Null:
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if (!firstOrSecondElement)
            {
                playerBanners[id].firstElementIcon.transform.parent.GetComponent<Image>().enabled = false;
            } else
            {
                playerBanners[id].secondElementIcon.transform.parent.GetComponent<Image>().enabled = false;
            }
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

        float minimumX = _myColl.bounds.max.x;
        float maximumX = _myColl.bounds.min.x;
        float minimumY = _myColl.bounds.max.y;
        float maximumY = _myColl.bounds.min.y;
        float largestDiff = 0;
        for (int i = 0; i < _cameraTargets.Length; i++)
        {
            if (!_cameraTargets[i].gameObject.activeSelf || !_cameraTargets[i].GetComponent<MeshRenderer>().enabled) //TODO: Very ugly check, improve it.
            {
                continue;
            }
            if (_cameraTargets[i].position.x < minimumX)
            {
                minimumX = Mathf.Max(_cameraTargets[i].position.x, _myColl.bounds.min.x);
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }
            if (_cameraTargets[i].position.x > maximumX)
            {
                maximumX = Mathf.Min(_cameraTargets[i].position.x, _myColl.bounds.max.x);
                if (maximumX - minimumX > largestDiff)
                {
                    largestDiff = maximumX - minimumX;
                }
            }

            if (_cameraTargets[i].position.y < minimumY)
            {
                minimumY = Mathf.Max(_cameraTargets[i].position.y, _myColl.bounds.min.y);
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
            if (_cameraTargets[i].position.y > maximumY)
            {
                maximumY = Mathf.Min(_cameraTargets[i].position.y, _myColl.bounds.max.y);
                if (maximumY - minimumY > largestDiff)
                {
                    largestDiff = maximumY - minimumY;
                }
            }
        }

        float XPos = minimumX + ((maximumX - minimumX) / 2);
        float YPos = minimumY + ((maximumY - minimumY) / 2);
        Vector3 targetPos = new Vector3(XPos + Glob.camXOffset, YPos + Glob.camYOffset, 0);
        Vector3 targetDiff = targetPos - _myCamera.transform.position;
        _myCamera.transform.position = new Vector3(_myCamera.transform.position.x + (targetDiff.x * Glob.camSpeed), _myCamera.transform.position.y + (targetDiff.y * Glob.camSpeed), Glob.camZOffset - (largestDiff / 2));

        //SCREENSHAKE!!!!!!!!!!!!
        //If the screen should shake.
        if (_shouldScreenShake)
        {
            //Store the start position (redundant, left by an older system, should be cleaned up.)
            float startPosX = _myCamera.transform.position.x;
            float startPosY = _myCamera.transform.position.y;

            //Displace the screen by a random amount in a random direction.
            _myCamera.transform.position = new Vector3(startPosX + ((((_shakeTime - (Time.time - _shakeStartTime)) / (_shakeTime / 100)) / 100) * _shakeDistance) * Random.Range(-1, 2), startPosY + ((((_shakeTime - (Time.time - _shakeStartTime)) / (_shakeTime / 100)) / 100) * _shakeDistance) * Random.Range(-1, 2), _myCamera.transform.position.z);

            //Apply a delay between displacements.
            if (Time.time - _shakeStartTime > _shakeTime)
            {
                _shouldScreenShake = false;
                _myCamera.transform.position = new Vector3(startPosX, startPosY, _myCamera.transform.position.z);
            }
        }
    }

    private void handleBoundaries()
    {

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(Glob.maxHealth);
                if(player.GetID()== 0)
                {
                    SoundManager.GetInstance().PlaySound(Glob.Player1FallSound);
                } else
                {
                    SoundManager.GetInstance().PlaySound(Glob.Player2FallSound);
                }
                
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }

    public void SetScreenShake(float shakeDist, float time)
    {
        _shakeDistance = shakeDist;
        _shakeTime = time;
        _shakeStartTime = Time.time;

        _shouldScreenShake = true;
    }
}
