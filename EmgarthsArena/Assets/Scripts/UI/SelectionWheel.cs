using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SelectionWheel : MonoBehaviour
{

    private const float continuousDelay = 0.5f;
    private const float scrollSpeed = 10; //per second
    private float lastScroll = 0;
    private bool waitingForContinuous = false;
    private bool scrollingContinuous = false;

    private string myAxis;

    public Sprite[] wheelImages;
    private Image _myImage;
    private int currentImage = 0;

    private string submitButton;
    public Button[] wheelButtons;
    public Vector3[] buttonPositions;

    public int startingRotation = 0;
    public bool resetOnEnable = true;

    // Use this for initialization
    void Start()
    {
        myAxis = EventSystem.current.GetComponent<StandaloneInputModule>().horizontalAxis;

        _myImage = GetComponent<Image>();
        if (wheelImages != null)
        {
            _myImage.sprite = wheelImages[currentImage];
        }
        selectWheelPosition(startingRotation);
    }

    void OnEnable()
    {
        if (_myImage == null || !resetOnEnable)
        {
            return;
        }
        selectWheelPosition(startingRotation);
    }

    private void selectWheelPosition(int pos)
    {
        currentImage = pos;
        _myImage.sprite = wheelImages[currentImage];

        for (int i = 0; i < wheelButtons.Length; i++)
        {
            wheelButtons[i].transform.position = new Vector3(0, -1000, 0);
        }

        wheelButtons[currentImage].transform.localPosition = buttonPositions[1];
        wheelButtons[currentImage].GetComponent<Image>().enabled = true;
        if (currentImage > 0)
        {
            wheelButtons[currentImage - 1].transform.localPosition = buttonPositions[0];
            wheelButtons[currentImage - 1].GetComponent<Image>().enabled = false;
        }
        if (currentImage < wheelButtons.Length - 1)
        {
            wheelButtons[currentImage + 1].transform.localPosition = buttonPositions[2];
            wheelButtons[currentImage + 1].GetComponent<Image>().enabled = false;
        }
        wheelButtons[currentImage].Select();
    }

    // Update is called once per frame
    void Update()
    {
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
                currentImage = wheelImages.Length - 1;
            }
            else if (currentImage < 0)
            {
                currentImage = 0;
            }

            selectWheelPosition(currentImage);

            SoundManager.GetInstance().PlayUISound();
        }
        else if (scrollingContinuous || waitingForContinuous)
        {
            waitingForContinuous = false;
            scrollingContinuous = false;
        }

    }
}
