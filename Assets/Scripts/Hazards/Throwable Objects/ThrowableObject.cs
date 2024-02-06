using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableObject", menuName = "ScriptableObjects/ThrowableObjectData", order = 1)]
public class ThrowableObject : ScriptableObject
{
    [SerializeField] float verticalSpeed;
    [SerializeField] float throwingTime;
    [SerializeField] ThrowableObjectType type;

    public float VerticalSpeed { get => verticalSpeed;}
    public ThrowableObjectType Type { get => type;}
}
public enum ThrowableObjectType
{
    Small = 0,
    Big
}
