using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] UnityEvent onPlayerCollision;

    [SerializeField] float speed;
    Transform initialTransform;
    private bool usable;
    [SerializeField] ObstacleType type;
    Collider _collider;

    public bool Usable { get => usable;}
    public ObstacleType Type { get => type;}
    public float Speed { get => speed;}

    private void Awake()
    {
        initialTransform = transform;
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _collider.enabled = true;
        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            onPlayerCollision.Invoke();
        }
    }

}
