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
    [SerializeField] List<Collider> _colliders;

    public bool Usable { get => usable; }
    public ObstacleType Type { get => type; }
    public float Speed { get => speed; }

    private void Awake()
    {
        initialTransform = transform;
    }

    private void OnEnable()
    {
        EnableColliders();

        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Vector3 currentPosition = transform.position;
            float newZAxisPosition = currentPosition.z + speed * Time.deltaTime;

            transform.position = new Vector3(initialTransform.position.x, currentPosition.y, newZAxisPosition);
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

    public void EnableColliders()
    {
        foreach (Collider item in _colliders)
        {
            item.isTrigger = false;
        }
    }

    public void DisableColliders()
    {
        foreach (Collider item in _colliders)
        {
            item.isTrigger = true;
        }
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
            GameEvents.instance.OnPlayerHitted.Invoke();
        }
    }

    private void OnDisable()
    {
        
    }

}
