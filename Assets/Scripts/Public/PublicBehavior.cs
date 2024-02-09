using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicBehavior : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        int initialState = Random.Range(1, 2);

        SetAnimation(initialState);
    }

    public void SetAnimation(int animationState)
    {
        animator.SetInteger("AnimationState" , animationState);
    }
}
