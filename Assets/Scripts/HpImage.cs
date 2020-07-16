using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HpImage : MonoBehaviour
{
    //Serialized Fields
    [SerializeField] Sprite[] hpSprites;
    SpriteRenderer hpManager;
    [SerializeField] int numLifes;
    [SerializeField] int currentLifes;

    void Start()
    {
        numLifes = hpSprites.Length + 1;
        currentLifes = numLifes;
        hpManager = GetComponent<SpriteRenderer>();
        hpManager.sprite = hpSprites[0];
    }

    public int GetHP()
    {
        return currentLifes;
    }

    public void DecreaseHP()
    {
        currentLifes--;
        if(currentLifes > 1)
        {
            hpManager.sprite = hpSprites[numLifes-currentLifes];
        }
        else
        {
            RemoveHp();
        }

    }
    public void RemoveHp()
    {
        Destroy(gameObject);
    }
}
