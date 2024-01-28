using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int round;
    [SerializeField] float distanceRequiredForRound;
    public float distanceTraveled , lastDistanceChackpoint;
    [SerializeField] GameObject gameOverPanel , newHighScoreView , obtainedScore;
    [SerializeField] TMP_Text scoreText , newHighScoreTXT, obtainedScoreTXT;

    [SerializeField] HighScoreBrehavior scoreBrehavior;
    public int Round { get => round; }

    ObstaclesSpawningManager obstaclesSpawningManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    private void Update()
    {
        distanceTraveled += Time.deltaTime;
        scoreText.text = distanceTraveled.ToString() + " mts";
        if (lastDistanceChackpoint < distanceRequiredForRound)
        {
            lastDistanceChackpoint = distanceTraveled;
        }
        else
        {
            lastDistanceChackpoint = 0;
            IncreaseRound();
        }
    }

    private void IncreaseRound()
    {
        round++;
        obstaclesSpawningManager.NewRoundBehavior();
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        if (distanceTraveled > scoreBrehavior.lastHighScore)
        {
            newHighScoreView.SetActive(true);
            newHighScoreTXT.text = distanceTraveled.ToString() + " mts";
            SaveScore();
        }
        else
        {
            obtainedScore.SetActive(true);
            obtainedScoreTXT.text = distanceTraveled.ToString() + " mts";
        }
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void PlayAgain()
    {
        ScencesManger.instance.Restart();
        ResumeGame();
    }
    public void SaveScore()
    {
        SaveSystem.SaveHighScore(distanceTraveled);
    }
}
