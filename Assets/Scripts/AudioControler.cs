using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    SoundButton soundButton;
    MusicButton musicButton;
    MusicPlayer musicPlayer;
    AudioSource audioSource;
    AudioSource buttonSwitch;
    float volume;
    bool music = true;
    bool sound = true;

    private void Awake()
    {
        StartSingleton();
    }

    private void StartSingleton()
    {
        if (FindObjectsOfType <AudioControler>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        soundButton = FindObjectOfType<SoundButton>();
        musicButton = FindObjectOfType<MusicButton>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
        audioSource = musicPlayer.GetComponent<AudioSource>();
        audioSource.ignoreListenerPause = true;
        volume = audioSource.volume;
        buttonSwitch = GetComponent<AudioSource>();

    }

    public void SoundControler()
    {
        if (sound)
        {
            sound = false;
            AudioListener.pause = true;
            soundButton.ChangeSprite();
            
        }
        else
        {
            sound = true;
            AudioListener.pause = false;
            soundButton.ChangeSprite();
            AudioSource.PlayClipAtPoint(buttonSwitch.clip,
                Camera.main.transform.position,
                buttonSwitch.volume);
        }

    }

    public void MusicControler()
    {
        if (music)
        {
            music = false;
            audioSource.volume = 0;
            musicButton.ChangeSprite();

        }
        else
        {
            music = true;
            audioSource.volume = volume;
            musicButton.ChangeSprite();
            AudioSource.PlayClipAtPoint(buttonSwitch.clip,
                Camera.main.transform.position,
                buttonSwitch.volume);
        }
    }
}
