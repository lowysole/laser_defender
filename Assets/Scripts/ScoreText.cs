using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text textField;
    [SerializeField] int score;

    // Start is called before the first frame update
    void Start()
    {
        textField = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            textField.text = FindObjectOfType<GameSession>().GetScore().ToString();
        } catch { }
    }
}
