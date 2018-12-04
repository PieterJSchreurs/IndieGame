using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GoBackButton : MonoBehaviour {

    public UnityEvent OnPressEvents;
    private string myButton;

    // Use this for initialization
    void Start () {
        myButton = EventSystem.current.GetComponent<StandaloneInputModule>().cancelButton;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(myButton))
        {
            OnPressEvents.Invoke();
        }
	}
}
