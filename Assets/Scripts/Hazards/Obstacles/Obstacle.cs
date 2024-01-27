using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        speed = 5;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Vector3 currentPosition = transform.position;
            float newZAxisPosition = currentPosition.z + speed * Time.deltaTime;

            transform.position = new Vector3(currentPosition.x, currentPosition.y, newZAxisPosition);
        }
    }
    public void ModifySpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
            transform.position = initialPosition;
        }
    }
}
