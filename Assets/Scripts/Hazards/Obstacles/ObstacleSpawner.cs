using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    static float obstacleSpeed;

    public static float ObstacleSpeed { get => obstacleSpeed; set => obstacleSpeed = value; }

    private void Awake()
    {
        obstacleSpeed = 5;
        foreach (GameObject item in obstacles)
        {
            item.GetComponent<Obstacle>().SetSpeed(obstacleSpeed);
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

    public static void ModifyObstacleSpeed(float newSpeed)
    {
        obstacleSpeed = newSpeed;
    }

    public void NewRoundBehavior()
    {
        foreach (GameObject item in obstacles)
        {
            item.GetComponent<Obstacle>().SetSpeed(obstacleSpeed);
        }

        GameManager gameManager = GameManager.instance;

        switch (gameManager.Round)
        {
            case 1:
                FirstRoundBehavior();
                break;

            case 2:
                SecondRoundBehavior();
                break;

            case 3:
                ThirdRoundBehavior();
                break;

            case 4:
                FourthRoundBehavior();
                break;

            case 5:
                FithRoundBehavior();
                break;

            default:
                break;
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
    private void FirstRoundBehavior()
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
    private void SecondRoundBehavior()
    {
        foreach (GameObject item in obstacles)
        {
            if (item.GetComponent<Obstacle>().Type == ObstacleType.tall)
            {
                item.GetComponent<Obstacle>().ModifyUsableState(true);
            }
        }
    }
    private void ThirdRoundBehavior()
    {
    }
    private void FourthRoundBehavior()
    {
    }
    private void FithRoundBehavior()
    {
    }


}
