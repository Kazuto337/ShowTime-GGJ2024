using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    [SerializeField] public UnityEvent OnPlayerHitted;

    private void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(this);
        }
        else instance = this;
    }
}
