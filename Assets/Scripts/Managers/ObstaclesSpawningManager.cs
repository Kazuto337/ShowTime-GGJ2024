using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawningManager : MonoBehaviour
{
    [Header("Properties")]

    [SerializeField] float spawningActivationRate;
    private float spawningTimer = 0;
    [SerializeField] ObstacleSpawner spawner1 , spawner2, spawner3;

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
        }
    }
}
