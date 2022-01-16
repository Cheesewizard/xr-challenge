using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }

    // Singleton Pattern
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    [SerializeField]
    private string scoreHeader = "SCORE";
    private int currentScore;

    public TextMeshProUGUI scoreText;


    public void UpdateScore(Pickup pickup)
    {
        var totalScore = currentScore += pickup.ScoreValue;
        scoreText.text = $"{scoreHeader}: {totalScore}";
    }
}
