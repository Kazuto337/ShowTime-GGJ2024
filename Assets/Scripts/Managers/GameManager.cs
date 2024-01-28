using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int round;
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
        if (lastDistanceChackpoint < 150)
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
        obstaclesSpawningManager.IncreaseSpeedPercentage();
    }
}
