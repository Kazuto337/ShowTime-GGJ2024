using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int round;
    [SerializeField] float distanceRequiredForRound;
    public float distanceTraveled , lastDistanceChackpoint;
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
        SaveScore();
        ResumeGame();
    }
    public void SaveScore()
    {

    }
}
