using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //parameters
    [SerializeField] int score;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
       if( FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}
