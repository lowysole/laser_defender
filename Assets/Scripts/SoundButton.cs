using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;
    bool currentSprite;

    // Start is called before the first frame update

    void Start()
    {
        currentSprite = true;
        GetComponent<Image>().sprite = spriteOn;
    }

    // Update is called once per frame

    public void ChangeSprite()
    {
        if (currentSprite)
        {
            currentSprite = false;
            GetComponent<Image>().sprite = spriteOff;
        }
        else
        {
            currentSprite = true;
            GetComponent<Image>().sprite = spriteOn;
        }
    }
}
