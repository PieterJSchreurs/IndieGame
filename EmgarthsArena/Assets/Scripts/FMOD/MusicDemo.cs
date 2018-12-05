using UnityEngine;
using System.Collections;

using FMODUnity; // For EventRef, RuntimeManager
using FMOD.Studio; // For EventInstance, ParameterInstance, EVENT_PROPERTY, STOP_MODE

public class MusicDemo : MonoBehaviour {
	// When using [EventRef], FMOD editor scripts show some extra event info in the inspector:
	[EventRef]
	public string Music;

	EventInstance musicEvent;
	ParameterInstance musicIntensity;
	float gameIntensity;
    bool initialized = false;

	void Start () {
		// Create an *instance* of the event given by the string [Music] 
		// (the bank containing this event should be loaded):
		musicEvent = RuntimeManager.CreateInstance (Music);
		// Store the intensity parameter of the music instance in [musicIntensity]:
		musicEvent.getParameter ("Parameter 1", out musicIntensity);
		// Start playing:
		musicEvent.start();

		gameIntensity = 0;
        initialized = true;

	}
	
	void Update () {
		
	}

	// Called when this component is destroyed (so also when the game object is destroyed):
	void OnDestroy() {
		if (!initialized) // Holds when the component is inactive
			return;
		// Stop the music:
		musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		// Tell FMOD Studio that this event instance can be destroyed:
		musicEvent.release ();
	}
}
