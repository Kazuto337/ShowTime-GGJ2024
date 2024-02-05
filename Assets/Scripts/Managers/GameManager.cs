using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GameEvents"), Space(35)]
    public UnityEvent OnNewRound;

    [Header("GameProperties")]
    private int round;
    [SerializeField] float distanceRequiredForRound;
    public float distanceTraveled , lastDistanceCheckpoint;

    [Header("UI Objects" ), Space(15f)]
    [SerializeField] HighScoreBrehavior scoreBrehavior;
    [SerializeField] GameObject gameOverPanel , newHighScoreView , obtainedScore;
    [SerializeField] TMP_Text scoreText , newHighScoreTXT, obtainedScoreTXT;

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
        OnNewRound.Invoke();
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
        Vector2 _return = new Vector2(player.transform.position.x , player.transform.position.z);
        return _return;
    }
}
