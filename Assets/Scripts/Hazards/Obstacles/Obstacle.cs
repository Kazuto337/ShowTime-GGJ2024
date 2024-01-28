using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    small = 0,
    normal,
    tall,
    wide
}
[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 initialPosition;
    private bool usable;
    [SerializeField] ObstacleType type;

    public bool Usable { get => usable;}
    public ObstacleType Type { get => type;}
    public float Speed { get => speed;}

    private void Start()
    {
        initialPosition = transform.position;
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
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void ModifyUsableState(bool newUsable)
    {
        usable = newUsable;
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
