using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> smallObstacles, tallObstacles, wideObstacles;
    public void Spawn()
    {
        GameObject obstacle = LoadObstacle();
        obstacle.transform.position = gameObject.transform.position;
        obstacle.SetActive(true);
    }

    private GameObject LoadObstacle()
    {
        int poolIndex = Random.Range(1, 4);
        GameObject obstacle = smallObstacles[Random.Range(0, smallObstacles.Count)];

        switch (poolIndex)
        {
            case 1:

                obstacle = smallObstacles[Random.Range(0, smallObstacles.Count)];
                while (obstacle.activeInHierarchy)
                {
                    obstacle = smallObstacles[Random.Range(0, smallObstacles.Count)];
                }
                break;

            case 2:
                obstacle = smallObstacles[Random.Range(0, smallObstacles.Count)];
                while (obstacle.activeInHierarchy)
                {
                    obstacle = tallObstacles[Random.Range(0, smallObstacles.Count)];
                }
                break;

            case 3:
                obstacle = smallObstacles[Random.Range(0, smallObstacles.Count)];
                while (obstacle.activeInHierarchy)
                {
                    obstacle = tallObstacles[Random.Range(0, smallObstacles.Count)];
                }
                break;

            default:
                return null;
        }

        return obstacle;
    }

    public void ModifyObstacleSpeeds(float newSpeed)
    {
        foreach (GameObject item in smallObstacles)
        {
            item.GetComponent<Obstacle>().ModifySpeed(newSpeed);
        }
        foreach (GameObject item in tallObstacles)
        {
            item.GetComponent<Obstacle>().ModifySpeed(newSpeed);
        }
        foreach (GameObject item in wideObstacles)
        {
            item.GetComponent<Obstacle>().ModifySpeed(newSpeed);
        }
    }
}
