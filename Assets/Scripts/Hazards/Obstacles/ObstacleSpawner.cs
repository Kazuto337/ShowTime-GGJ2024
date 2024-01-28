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
        AleatorizarListas();
        ActivateFirstRoundBehavior();
    }
    private void AleatorizarListas()
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

    public void ModifyObstacleSpeeds()
    {
        foreach (GameObject item in obstacles)
        {
            item.GetComponent<Obstacle>().SetSpeed(obstacleSpeed);
        }
    }
    private void ActivateFirstRoundBehavior()
    {
        foreach (GameObject item in obstacles)
        {
            if (item.GetComponent<Obstacle>().Type == ObstacleType.small || item.GetComponent<Obstacle>().Type == ObstacleType.wide)
            {
                item.GetComponent<Obstacle>().ModifyUsableState(true);
            }
            else
            {
                item.GetComponent<Obstacle>().ModifyUsableState(false);
            }
        }
    }
}
