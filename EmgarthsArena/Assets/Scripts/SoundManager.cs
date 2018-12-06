using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance;
    private string BackGroundMusic = "event:/Backgroundsong/Backgroundtrack";

    EventInstance musicEvent;
    ParameterInstance musicIntensity;
    EventInstance UISoundEffect;
    EventInstance UISubmitSoundEffect;
    private PLAYBACK_STATE playBackState;
    float gameIntensity;
    bool initialized = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        StartBackgroundMusic();
        PlaySound(Glob.WelcomeSound);
        UISoundEffect = RuntimeManager.CreateInstance(Glob.UIHoveringSound);
        UISubmitSoundEffect = RuntimeManager.CreateInstance(Glob.UISelectingSound);
    }

    public static SoundManager GetInstance()
    {
        if (_instance == null)
        {
            Debug.Log("There is no instance yet, this is not supposed to happen.");
            _instance = new SoundManager();
        }
        return _instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartBackgroundMusic()
    {
        musicEvent = RuntimeManager.CreateInstance(BackGroundMusic);
        // Store the intensity parameter of the music instance in [musicIntensity]:
        musicEvent.getParameter("Parameter 1", out musicIntensity);
        // Start playing:
        musicEvent.start();
        gameIntensity = 0;
        initialized = true;
    }

    public void StopBackGroundMusic()
    {
        if (!initialized) // Holds when the component is inactive
            return;
        // Stop the music:
        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // Tell FMOD Studio that this event instance can be destroyed:
        musicEvent.release();

    }

    public void SetBackGroundMusicIntensity(float pIntensity)
    {
        gameIntensity = pIntensity;
        musicIntensity.setValue(gameIntensity);
    }

    public void PlayUISound()
    {
        UISoundEffect.getPlaybackState(out playBackState);
        if(playBackState != PLAYBACK_STATE.PLAYING)
        {
            UISoundEffect.start();
        }
    }

    public void PlayUISubmitSound()
    {
        UISubmitSoundEffect.getPlaybackState(out playBackState);
        if (playBackState != PLAYBACK_STATE.PLAYING)
        {
            UISubmitSoundEffect.start();
        }
    }

    public void PlaySound(string pSound)
    {
        RuntimeManager.PlayOneShot(pSound);
    }
}
