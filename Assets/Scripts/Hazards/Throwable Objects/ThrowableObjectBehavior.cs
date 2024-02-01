using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectBehavior : MonoBehaviour
{
    [SerializeField] ThrowableObject objectData;

    float speed, timeBeforeFalling;
    Animator animator;
    [SerializeField] Vector2 dropingRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
        speed = objectData.VerticalSpeed;
        timeBeforeFalling = objectData.TimeBeforeFalling;
    }

    public void ThrowObject()
    {

    }

    public void DropObject()
    {

    }


}
