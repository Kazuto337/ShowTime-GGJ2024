using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public static ConveyerBelt Instance;

    float speed = 2;
    [SerializeField] GameObject belt;
    Material beltShader;

    public float Speed { get => speed; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        beltShader = belt.GetComponent<Renderer>().material;
        beltShader.SetFloat("_RotationSpeed", speed);
    }

    private void OnEnable()
    {
        speed = 2;
    }
    public void ModifySpeed(float newSpeed)
    {
        speed = newSpeed;
        beltShader.SetFloat("_RotationSpeed", newSpeed);
    }

}
