using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SliderScript : Selectable {

    public Slider MySlider;

    private string myAxis;

    private const float continuousDelay = 0.5f;
    private const float scrollSpeed = 50; //per second
    private float lastScroll = 0;
    private bool waitingForContinuous = false;
    private bool scrollingContinuous = false;

    // Use this for initialization
    void Start () {
        myAxis = EventSystem.current.GetComponent<StandaloneInputModule>().horizontalAxis;

    }

    //Use this to check what Events are happening
    BaseEventData m_BaseEvent;

    void Update()
    {
        //Check if the GameObject is being highlighted
        if (IsHighlighted(m_BaseEvent) == true)
        {
            if (Mathf.Abs(Input.GetAxis(myAxis)) > 0.5f)
            {
                float dir = Mathf.Sign(Input.GetAxis(myAxis));
                if (!waitingForContinuous)
                {
                    MySlider.value += dir / 100f;
                    lastScroll = Time.time;
                    waitingForContinuous = true;
                }
                else if (Time.time - lastScroll >= continuousDelay || scrollingContinuous)
                {
                    if (!scrollingContinuous)
                    {
                        MySlider.value += dir / 100f;
                        lastScroll = Time.time;
                        scrollingContinuous = true;
                    }
                    else if (Time.time - lastScroll >= 1 / scrollSpeed)
                    {
                        MySlider.value += dir / 100f;
                        lastScroll = Time.time;
                    }
                }
            }
            else if (scrollingContinuous || waitingForContinuous)
            {
                waitingForContinuous = false;
                scrollingContinuous = false;
            }
        }
    }
}
