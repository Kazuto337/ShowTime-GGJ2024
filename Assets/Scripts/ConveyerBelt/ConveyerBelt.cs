using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public static float speed;

    public static void ModifySpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
