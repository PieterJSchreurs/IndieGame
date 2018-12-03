using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class FakeLoadImageFill : MonoBehaviour {

    private Image myImage;
    [Tooltip("Fill in the times you want to image to have an interval at here. (i.e. Entering a 1 will make the image have an interval after 1 second.)")]
    public float[] loadIntervals;
    [Tooltip("The range the intervals can vary from their given values in seconds. (So if loadIntervals[0] is set to run 2 seconds in, and intervalRange is set to 1, loadIntervals[0] will run between 1-3 seconds in.")]
    public float intervalRange;
    [Tooltip("The minimum duration of an interval in seconds.")]
    public float intervalTimeMin;
    [Tooltip("The maximum duration of an interval in seconds.")]
    public float intervalTimeMax;
    [Tooltip("The time it takes to fill the image in seconds. Excluding intervals.")]
    public float loadTime;
    [Tooltip("The chance for each frame to not fill the image. This value gets flipped during an interval.")]
    [Range(0.1f, 0.99f)]
    public float stutterChance;

    public UnityEvent OnEnableEvents;
    public UnityEvent OnDoneLoading;

    // Use this for initialization
    void Awake () {
        myImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        OnEnableEvents.Invoke();
        StartCoroutine(fakeLoadStutter());
    }
	
    private IEnumerator fakeLoad() //Completely stops the fill during intervals.
    {
        int i;
        int j;

        bool shouldDoStep = true;

        float loadStep = (1 / loadTime) / (1 / Time.fixedDeltaTime);
        float intervalFrames = 0;
        float[] intervalTime = new float[loadIntervals.Length];
        float[] tempLoadIntervals = new float[loadIntervals.Length]; ;
        for (i = 0; i < loadIntervals.Length; i++)
        {
            tempLoadIntervals[i] = loadIntervals[i];
            tempLoadIntervals[i] += intervalFrames;
            tempLoadIntervals[i] += Random.Range(-intervalRange, intervalRange);

            intervalTime[i] = Random.Range(intervalTimeMin, intervalTimeMax);
            intervalFrames += intervalTime[i];
        }
        intervalFrames = intervalFrames / Time.fixedDeltaTime;


        myImage.fillAmount = 0;

        for (i = 0; i < (loadTime / Time.fixedDeltaTime) + intervalFrames; i++)
        {
            shouldDoStep = true;
            for (j = 0; j < tempLoadIntervals.Length; j++)
            {
                if (i / (1 / Time.fixedDeltaTime) > tempLoadIntervals[j] && i / (1 / Time.fixedDeltaTime) < tempLoadIntervals[j] + intervalTime[j])
                {
                    shouldDoStep = false;
                    break;
                }
            }
            if (Random.Range(0f, 1f) < stutterChance)
            {
                shouldDoStep = false;
                i--;
            }
            if (shouldDoStep)
            {
                myImage.fillAmount += loadStep;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator fakeLoadStutter() //Flips the 'stutterChance' during an interval, resulting in the bar filling up slower.
    {
        int i;
        int j;

        bool shouldDoStep = true;
        bool isAtInterval = false;

        float loadStep = (1 / loadTime) / (1 / Time.fixedDeltaTime);
        //float intervalFrames = 0;
        float[] intervalTime = new float[loadIntervals.Length];
        float[] tempLoadIntervals = new float[loadIntervals.Length]; ;
        for (i = 0; i < loadIntervals.Length; i++)
        {
            tempLoadIntervals[i] = loadIntervals[i];
            //tempLoadIntervals[i] += intervalFrames;
            tempLoadIntervals[i] += Random.Range(-intervalRange, intervalRange);

            intervalTime[i] = Random.Range(intervalTimeMin, intervalTimeMax);
            //intervalFrames += intervalTime[i];
        }
        //intervalFrames = intervalFrames / Time.fixedDeltaTime;


        myImage.fillAmount = 0;

        for (i = 0; i < loadTime / Time.fixedDeltaTime; i++)
        {
            shouldDoStep = true;
            isAtInterval = false;
            for (j = 0; j < tempLoadIntervals.Length; j++)
            {
                if (i / (1 / Time.fixedDeltaTime) > tempLoadIntervals[j] && i / (1 / Time.fixedDeltaTime) < tempLoadIntervals[j] + intervalTime[j])
                {
                    isAtInterval = true;
                    if (Random.Range(0f, 1f) > stutterChance)
                    {
                        shouldDoStep = false;
                        i--;
                    }
                    break;
                }
            }
            if (Random.Range(0f, 1f) < stutterChance && !isAtInterval)
            {
                shouldDoStep = false;
                i--;
            }
            if (shouldDoStep)
            {
                myImage.fillAmount += loadStep;
            }
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("done");
        OnDoneLoading.Invoke();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
