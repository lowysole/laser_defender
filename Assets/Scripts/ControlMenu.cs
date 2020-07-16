using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    bool status = false;
    AudioSource buttonSound;

    private void Start()
    {
        gameObject.SetActive(false);
        buttonSound = FindObjectOfType<Level>().GetComponent<AudioSource>();
    }

    public void ChangeStatus()
    {
        AudioSource.PlayClipAtPoint(buttonSound.clip,
            Camera.main.transform.position,
            buttonSound.volume);

        if (status){
            status = false;
            turnOffMenu();
        } else
        {
            status = true;
            turnOnMenu();
        }
    }

    private void turnOnMenu()
    {
        gameObject.SetActive(true);
    }
   
    public void turnOffMenu()
    {
        gameObject.SetActive(false);
    }
    
}
