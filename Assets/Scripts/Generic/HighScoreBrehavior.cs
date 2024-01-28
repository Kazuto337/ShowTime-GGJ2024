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
        highScore.text = SaveSystem.LoadHighScore().Score.ToString();
        lastHighScore = SaveSystem.LoadHighScore().Score;
    }
}
