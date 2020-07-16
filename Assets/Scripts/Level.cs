using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    AudioSource audioButton;

    private void Start()
    {
        audioButton = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
    AudioSource.PlayClipAtPoint(audioButton.clip,
            Camera.main.transform.position,
            audioButton.volume);
    }

    public void LoadGameOver()
    {
        String str = "GameOver";
        StartCoroutine(WaitForLoad(str)); 
    }

    public void LoadGameScene()
    {
        FindObjectOfType<GameSession>().ResetScore();
        PlaySound();
        String str = "Game";
        StartCoroutine(WaitForLoad(str));
    }

    public void LoadStartMenu()
    {
        PlaySound();
        String str = "MainMenu";
        StartCoroutine(WaitForLoad(str));
    }

    IEnumerator WaitForLoad(String str)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(str);
    }

}
