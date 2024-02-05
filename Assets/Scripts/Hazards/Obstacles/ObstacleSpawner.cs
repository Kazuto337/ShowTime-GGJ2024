using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;


    private void Awake()
    {
        foreach (GameObject item in obstacles)
        {
            item.GetComponent<Obstacle>().SetSpeed(ConveyerBelt.speed);
        }
    }

    private void Start()
    {
        RandomizeObstacleList();
        DefaultRoundBehavior();
    }
    private void RandomizeObstacleList()
    {
        System.Random rnd = new System.Random();

        int n = obstacles.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            (obstacles[k], obstacles[n]) = (obstacles[n], obstacles[k]);
        }
    }
    public void Spawn()
    {
        GameObject obstacle = LoadObstacle();
        obstacle.transform.position = gameObject.transform.position;
        obstacle.SetActive(true);
    }

    private GameObject LoadObstacle()
    {
        int poolIndex = Random.Range(0, obstacles.Count);

        while (obstacles[poolIndex].GetComponent<Obstacle>().Usable == false)
        {
            poolIndex = Random.Range(0, obstacles.Count);
        }
        return obstacles[poolIndex];
    }
    public void NewRoundBehavior()
    {
        foreach (GameObject item in obstacles)
        {
            item.GetComponent<Obstacle>().SetSpeed(ConveyerBelt.speed);
        }
    }
    public void NewRoundBehavior(int currentRound)
    {
        switch (currentRound)
        {
            case 1:
                ActivateSmallObstacles();
                break;

            case 2:
                ActivateTallObstacles();
                break;

            default:
                return;
        }
    }
    private void DefaultRoundBehavior()
    {
        foreach (GameObject item in obstacles)
        {
            bool a = item.GetComponent<Obstacle>().Type == ObstacleType.wide;
            bool b = item.GetComponent<Obstacle>().Type == ObstacleType.normal;

            if (a || b)
            {
                item.GetComponent<Obstacle>().ModifyUsableState(true);
            }
            else
            {
                item.GetComponent<Obstacle>().ModifyUsableState(false);
            }
        }
    }
    private void ActivateSmallObstacles()
    {
        foreach (GameObject item in obstacles)
        {
            bool a = item.GetComponent<Obstacle>().Type == ObstacleType.small;
            bool b = item.GetComponent<Obstacle>().Type == ObstacleType.wide;
            bool c = item.GetComponent<Obstacle>().Type == ObstacleType.normal;

            if (a || b || c)
            {
                item.GetComponent<Obstacle>().ModifyUsableState(true);
            }
            else
            {
                item.GetComponent<Obstacle>().ModifyUsableState(false);
            }
        }
    }
    private void ActivateTallObstacles()
    {
        foreach (GameObject item in obstacles)
        {
            if (item.GetComponent<Obstacle>().Type == ObstacleType.tall)
            {
                item.GetComponent<Obstacle>().ModifyUsableState(true);
            }
        }
    }
}
