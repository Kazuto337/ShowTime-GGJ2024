using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GameEvents")]
    public UnityEvent OnNewRound;

    [Header("GameProperties"), Space(15)]
    [SerializeField] float distanceRequiredForRound;
    private int round;
    public float distanceTraveled, lastDistanceCheckpoint;
    [SerializeField, Range(10f, 50)] float speedIncreasePercentage;
    [SerializeField, Range(1, 10)] float percentageIncreaseIndex;

    [Header("UI Objects"), Space(15f)]
    [SerializeField] HighScoreBrehavior scoreBrehavior;
    [SerializeField] GameObject gameOverPanel, newHighScoreView, obtainedScore;
    [SerializeField] TMP_Text scoreText, newHighScoreTXT, obtainedScoreTXT;

    [Header("GameObjects")]
    [SerializeField] GameObject player;
    public int Round { get => round; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        distanceTraveled += Time.deltaTime;
        scoreText.text = distanceTraveled.ToString("F2") + " mts";
        if (lastDistanceCheckpoint < distanceRequiredForRound)
        {
            lastDistanceCheckpoint = distanceTraveled;
            return;
        }

        distanceRequiredForRound += distanceRequiredForRound;
        IncreaseRound();
    }

    private void IncreaseRound()
    {
        round++;
        IncreaseConveyerBeltSpeed();
        OnNewRound.Invoke();
        IncreaseSpeedPercentage();
    }
    private void IncreaseConveyerBeltSpeed()
    {
        float newSpeed = ConveyerBelt.speed + (ConveyerBelt.speed * (speedIncreasePercentage / 100));
        Debug.Log("Speed: " + newSpeed);
        ConveyerBelt.ModifySpeed(newSpeed);
    }
    private void IncreaseSpeedPercentage()
    {
        speedIncreasePercentage += percentageIncreaseIndex;
    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        if (distanceTraveled > scoreBrehavior.lastHighScore)
        {
            newHighScoreView.SetActive(true);
            newHighScoreTXT.text = distanceTraveled.ToString("F2") + " mts";
            SaveScore();
        }
        else
        {
            obtainedScore.SetActive(true);
            obtainedScoreTXT.text = distanceTraveled.ToString("F2") + " mts";
        }
        PauseGame();
    }

    #region PauseSettings
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
    }
    public void SaveScore()
    {
        SaveSystem.SaveHighScore(distanceTraveled);
    }
    #endregion

    public Vector2 GetPlayerPosition()
    {
        print("GM playerPosition = " + player.transform.position);
        Vector2 _return = new Vector2(player.transform.position.x, player.transform.position.z);
        return _return;
    }
}
