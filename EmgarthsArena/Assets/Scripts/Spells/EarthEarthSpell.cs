using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarthSpell : Spell {

    private const int maxRocks = 7;
    private const int rockRemainTime = 30;
    private Vector3 _velo;
    private int _rocksLeft = 0;

    private struct rockVars
    {
        public GameObject _rock;
        public float _lastVelocityX;
        public float _lastVelocityY;
        public bool _standingStill;
        public float _standingStillStartTime;
    }
    private rockVars[] rocks = new rockVars[maxRocks];

    private void InitializeSpell()
    {
        knockback = 50;
        damage = 20;
        castTime = 0.5f;
        manaDrain = 70;
        spellType = SpellDatabase.SpellType.SolidObject;
        attackType = SpellDatabase.AttackType.Heavy;
    }

    // Use this for initialization
    void Start () {
        base.Start();
        _velo = -_rb.transform.up * Glob.EarthEarthSpeed;
        InitializeSpell();

        StartCoroutine(launchRocks(maxRocks));
    }

    private IEnumerator launchRocks(int amount, int startIndx = 0)
    {
        for (int i = startIndx; i < startIndx + amount; i++)
        {
            if (_isPaused)
            {
                _rocksLeft = maxRocks - i;
                break;
            }
            rocks[i] = new rockVars();
            rocks[i]._rock = Instantiate(Resources.Load<GameObject>(Glob.RockPrefab), transform.position, new Quaternion());
            rocks[i]._rock.GetComponent<Spell>().SetPlayer(myPlayer);
            SceneManager.GetInstance().AddMovingObject(rocks[i]._rock.GetComponent<MovingObject>());
            rocks[i]._standingStill = false;
            rocks[i]._lastVelocityX = 0;
            rocks[i]._lastVelocityY = 0;
            rocks[i]._standingStillStartTime = 0;

            //rocks[i]._rock.GetComponent<Rigidbody2D>().velocity = new Vector3((i - 3)*2, 20, 0);
            rocks[i]._rock.GetComponent<Rigidbody2D>().velocity = _velo + (new Vector3(-_velo.y, _velo.x, 0) * Random.Range(-0.3f, 0.3f));

            _rocksLeft = 0;
            yield return new WaitForSeconds(0.15f);
            
        }
        yield return null;
    }

    protected override void Move(bool isFixed)
    {
        if (!isFixed)
        {
            int rocksAlive = maxRocks;
            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i]._rock == null)
                {
                    rocksAlive--;
                    if (rocksAlive <= 0)
                    {
                        SceneManager.GetInstance().RemoveMovingObject(this);
                        Destroy(gameObject);
                        break;
                    }
                    continue;
                }
                if (Mathf.Abs(rocks[i]._lastVelocityX) - Mathf.Abs(rocks[i]._rock.GetComponent<Rigidbody2D>().velocity.x) > 10f) //If the rock is brought to a sudden stop horizontally.
                {
                    SceneManager.GetInstance().RemoveMovingObject(rocks[i]._rock.GetComponent<MovingObject>());
                    Destroy(rocks[i]._rock);
                }
                if (Mathf.Abs(rocks[i]._rock.GetComponent<Rigidbody2D>().velocity.x) < 0.3f && Mathf.Abs(rocks[i]._rock.GetComponent<Rigidbody2D>().velocity.y) < 0.3f) //If the rock is stationary for 2 seconds
                {
                    if (!rocks[i]._standingStill)
                    {
                        rocks[i]._standingStill = true;
                        rocks[i]._standingStillStartTime = Time.time;
                    }
                    if (Time.time - rocks[i]._standingStillStartTime >= rockRemainTime)
                    {
                        SceneManager.GetInstance().RemoveMovingObject(rocks[i]._rock.GetComponent<MovingObject>());
                        Destroy(rocks[i]._rock);
                    }
                }
                else
                {
                    rocks[i]._standingStill = false;
                }
                rocks[i]._lastVelocityX = rocks[i]._rock.GetComponent<Rigidbody2D>().velocity.x;
                rocks[i]._lastVelocityY = rocks[i]._rock.GetComponent<Rigidbody2D>().velocity.y;
            }
        }
    }

    protected override void HandleCollision(Collision2D collision)
    {
        base.HandleCollision(collision);
    }

    protected override void HandleExplosion()
    {

    }

    public override float GetCastTime()
    {
        if (castTime == -1)
        {
            InitializeSpell();
        }
        return castTime;
    }

    public override float GetManaCost()
    {
        if(manaDrain == -1)
        {
            InitializeSpell();
        }
        return manaDrain;
    }


    // Update is called once per frame
    void Update () {
        if (!_isPaused)
        {
            Move(false);
            if (_rocksLeft != 0)
            {
                Debug.Log("Spawning rocks!");
                StartCoroutine(launchRocks(_rocksLeft, maxRocks - _rocksLeft));
            }
        }
    }
}
