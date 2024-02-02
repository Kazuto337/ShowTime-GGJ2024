using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableObject", menuName = "ScriptableObjects/ThrowableObjectData", order = 1)]
public class ThrowableObject : ScriptableObject
{
    [SerializeField] float verticalSpeed;
    [SerializeField] float throwingTime;

    public float VerticalSpeed { get => verticalSpeed;}
}
