using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public static float speed = 2;

    private void OnEnable()
    {
        speed = 2;
    }

    public static void ModifySpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
