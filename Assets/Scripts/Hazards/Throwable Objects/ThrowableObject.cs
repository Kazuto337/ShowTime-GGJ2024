using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableObject", menuName = "ScriptableObjects/ThrowableObjectData", order = 1)]
public class ThrowableObject : ScriptableObject
{
    [SerializeField] float verticalSpeed;
    [SerializeField] float timeBeforeFalling;

    public float VerticalSpeed { get => verticalSpeed;}
    public float TimeBeforeFalling { get => timeBeforeFalling;}
}
