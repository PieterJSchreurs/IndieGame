using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SelectionWheel : MonoBehaviour {

    private const float continuousDelay = 0.5f;
    private const float scrollSpeed = 10; //per second
    private float lastScroll = 0;
    private bool waitingForContinuous = false;
    private bool scrollingContinuous = false;

    private string myAxis;

    public Sprite[] wheelImages;
    private Image _myImage;
    private int currentImage = 0;

	// Use this for initialization
	void Start () {
        myAxis = EventSystem.current.GetComponent<StandaloneInputModule>().horizontalAxis;

        _myImage = GetComponent<Image>();
        if (wheelImages != null)
        {
            _myImage.sprite = wheelImages[currentImage];
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(Input.GetAxis(myAxis)) > 0.5f)
        {
            int dir = Mathf.RoundToInt(Mathf.Sign(Input.GetAxis(myAxis)));
            if (!waitingForContinuous)
            {
                currentImage += dir;
                lastScroll = Time.time;
                waitingForContinuous = true;
            }
            else if (Time.time - lastScroll >= continuousDelay || scrollingContinuous)
            {
                if (!scrollingContinuous)
                {
                    currentImage += dir;
                    lastScroll = Time.time;
                    scrollingContinuous = true;
                }
                else if (Time.time - lastScroll >= 1 / scrollSpeed)
                {
                    currentImage += dir;
                    lastScroll = Time.time;
                }
            }
            if (currentImage >= wheelImages.Length)
            {
                currentImage = 0;
            } else if (currentImage < 0)
            {
                currentImage = wheelImages.Length - 1;
            }
            _myImage.sprite = wheelImages[currentImage];
            Debug.Log("Horizontal movement!");
        }
        else if (scrollingContinuous || waitingForContinuous)
        {
            waitingForContinuous = false;
            scrollingContinuous = false;
        }

	}
}
