using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectsManager : MonoBehaviour
{
    [SerializeField] List<ThrowableObjectBehavior> throwableObjects;
    [SerializeField, Range(0.25f, 5)] float spawnRate;
    float spawnTimer;

    bool isActive = false;


    private void Start()
    {
        isActive = false;
        RandomizeObstacleList();
    }
    private void Update()
    {
        if (isActive)
        {
            SpawnRateBehavior();
        }
    }

    private void RandomizeObstacleList()
    {
        System.Random rnd = new System.Random();

        int n = throwableObjects.Count;
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            (throwableObjects[k], throwableObjects[n]) = (throwableObjects[n], throwableObjects[k]);
        }
    }

    private void SpawnRateBehavior()
    {
        if (spawnTimer < spawnRate)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                Spawn();
                spawnTimer = 0;
            }
        }
        else if (spawnTimer >= spawnRate)
        {
            Spawn();
            spawnTimer = 0;
        }
    }

    private void Spawn()
    {
        int index = Random.Range(1, 2);

        ThrowableObjectBehavior selectedObject = SelectObject();
        ThrowableObjectBehavior selectedObject1 = SelectObject();

        if (selectedObject == null)
        {
            return;
        }

        if (selectedObject == selectedObject1)
        {
            OnSelectedObjectUnavailable(selectedObject);
        }

        switch (index)
        {
            case 1:

                selectedObject.ThrowObject();

                Debug.Log("Throw Object: " + selectedObject.name);
                break;

            case 2:

                selectedObject1.ThrowObject();
                selectedObject.ThrowObject();

                Debug.Log("Throw Object: " + selectedObject.name);
                Debug.Log("Throw Object: " + selectedObject1.name);

                break;
        }
    }

    private ThrowableObjectBehavior SelectObject()
    {
        int objectsAvailable = 0;
        foreach (ThrowableObjectBehavior item in throwableObjects)
        {
            if (item.CanThrow)
            {
                objectsAvailable++;
            }
        }

        if (objectsAvailable < 1)
        {
            Debug.LogWarning("No ThrowableObjects Avalailable");
            return null;
        }

        int objectIndex = Random.Range(0, throwableObjects.Count - 1);
        while (!throwableObjects[objectIndex].CanThrow)
        {
            objectIndex = Random.Range(0, throwableObjects.Count - 1);
            if (throwableObjects[objectIndex].CanThrow)
            {
                break;
            }
        }

        return throwableObjects[objectIndex];
    }

    private ThrowableObjectBehavior OnSelectedObjectUnavailable(ThrowableObjectBehavior firstObjectSelected)
    {
        foreach (ThrowableObjectBehavior item in throwableObjects)
        {
            if (item.CanThrow == true && firstObjectSelected != item)
            {
                return item;
            }
        }

        return null;
    }
    public void NewRoundBehavior()
    {
        int currentRound = GameManager.instance.Round;

        if (currentRound > 4)
        {
            spawnRate -= spawnRate * 0.10f;
            return;
        }

        switch (currentRound)
        {
            case 3:
                isActive = true;
                ActivateSmallObjects();
                break;

            case 4:
                ActivateBigObjects();
                break;

            default:
                return;
        }
    }
    private void ActivateSmallObjects()
    {
        foreach (ThrowableObjectBehavior item in throwableObjects)
        {
            if (item.GetObjectType() == ThrowableObjectType.Small)
            {
                item.ModifyThrowStatus(true);
            }
        }
    }
    private void ActivateBigObjects()
    {
        foreach (ThrowableObjectBehavior item in throwableObjects)
        {
            if (item.GetObjectType() == ThrowableObjectType.Big)
            {
                item.ModifyThrowStatus(true);
            }
        }
    }
}
