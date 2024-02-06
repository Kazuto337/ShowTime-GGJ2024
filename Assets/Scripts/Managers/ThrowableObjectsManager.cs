using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectsManager : MonoBehaviour
{
    [SerializeField] List<ThrowableObjectBehavior> throwableObjects;
    [SerializeField , Range(0.25f , 5)]float spawnRate;
    float spawnTimer;

    bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            SpawnRateBehavior(); 
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
    }

    private void Spawn()
    {
        int index = Random.Range(1, 2);        

        switch (index)
        {
            case 1:
                SelectObject().ThrowObject();
                break;

            case 2:

                ThrowableObjectBehavior selectedObject = SelectObject();
                ThrowableObjectBehavior selectedObject1 = null;

                while(selectedObject == SelectObject())
                {
                    selectedObject1 = SelectObject();
                    if (selectedObject1 != selectedObject)
                    {
                        selectedObject1.ThrowObject();
                        break;
                    }
                }
                selectedObject1.ThrowObject();
                selectedObject.ThrowObject();                
                break;
        }
    }   

    public ThrowableObjectBehavior SelectObject()
    {
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
