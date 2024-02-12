using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawningManager : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField, Range(0.25f, 5)] float spawningActivationRate;
    private float spawningTimer = 0;
    [SerializeField] ObstacleSpawner spawner1, spawner2, spawner3;

    private void Start()
    {
        spawningTimer = spawningActivationRate;
    }

    private void Update()
    {
        if (spawningTimer < spawningActivationRate)
        {
            spawningTimer += Time.deltaTime * 1;
        }
        else
        {
            ActivateSpawning();
            spawningTimer = 0;
        }
    }
    public void ActivateSpawning()
    {
        int spawningIndex = Random.Range(0, 3);

        switch (spawningIndex)
        {
            case 0:
                spawner1.Spawn();
                break;
            case 1:
                spawner2.Spawn();
                break;
            case 2:
                spawner3.Spawn();
                break;
            default:
                Debug.LogWarning("Spawner Selected = " + spawningIndex);
                break;
        }
    }

    //Invoke By GameManager
    public void NewRoundBehavior()
    {
        SpawnersNewRoundBehavior();
        DecreaseSpawningRate();
    }
    private void DecreaseSpawningRate()
    {
        spawningActivationRate -= spawningActivationRate * 0.25f;
    }
    private void SpawnersNewRoundBehavior()
    {
        int currentRound = GameManager.instance.Round;

        if (currentRound > 2)
        {
            spawner1.NewRoundBehavior();
            spawner2.NewRoundBehavior();
            spawner3.NewRoundBehavior();

            return;
        }    

        spawner1.NewRoundBehavior(currentRound);
        spawner2.NewRoundBehavior(currentRound);
        spawner3.NewRoundBehavior(currentRound);
    }
}
