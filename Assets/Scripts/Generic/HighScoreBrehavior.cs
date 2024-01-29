using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreBrehavior : MonoBehaviour
{
    [SerializeField] TMP_Text highScore;
    public float lastHighScore;
    void Start()
    {
        if (highScore != null)
        {
            highScore.text = SaveSystem.LoadHighScore().Score.ToString("F2") + " mts"; 
        }
        lastHighScore = SaveSystem.LoadHighScore().Score;
    }
}
