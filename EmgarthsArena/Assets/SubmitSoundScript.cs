using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SubmitSoundScript : MonoBehaviour
{

    private string submitButton;

    // Use this for initialization
    void Start()
    {
        submitButton = EventSystem.current.GetComponent<StandaloneInputModule>().submitButton;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(submitButton))
        {
            SoundManager.GetInstance().PlayUISubmitSound();
        }
    }
}
