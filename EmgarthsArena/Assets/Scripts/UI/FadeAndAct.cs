using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeAndAct : MonoBehaviour {

    private RawImage myImage;
    private Color startingColor;

    private float enableTime;

    public float FadeAfterSeconds = 5;
    public float FadeSpeed = 1;
    public bool ShouldFadeIn = true;

    private bool isFading;
    private bool hasFaded = false;

    public UnityEvent OnFaded;

	// Use this for initialization
	void Start () {
        myImage = GetComponent<RawImage>();
        startingColor = myImage.color;
        enableTime = Time.time;
    }

    void OnEnable()
    {
        enableTime = Time.time;
    }

    public void ResetFadeTimer()
    {
        enableTime = Time.time;
    }

    public void ResetFade()
    {
        myImage.color = startingColor;
        enableTime = Time.time;
        hasFaded = false;
        isFading = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFading)
        {
            if (ShouldFadeIn)
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a + (Time.deltaTime / FadeSpeed));
                if (myImage.color.a >= 1)
                {
                    //blackPanel.gameObject.SetActive(false);
                    isFading = false;
                    hasFaded = true;
                    OnFaded.Invoke();
                }
            }
            else
            {
                myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, myImage.color.a - (Time.deltaTime / FadeSpeed));
                if (myImage.color.a <= 0)
                {
                    isFading = false;
                    hasFaded = true;
                    OnFaded.Invoke();
                }
            }
        } else if (!hasFaded)
        {
            if (Time.time - enableTime >= FadeAfterSeconds)
            {
                FadeToBlack(true, FadeSpeed);
            }
        }
    }

    public void FadeToBlack(bool toggle, float time)
    {
        FadeSpeed = time;
        ShouldFadeIn = toggle;
        if (ShouldFadeIn)
        {
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0);
        }
        else
        {
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 1);
        }
        isFading = true;
    }
}
