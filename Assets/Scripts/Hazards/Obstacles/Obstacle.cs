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

    [SerializeField] MeshRenderer _renderer;
    Material[] _defaultMaterials;
    [SerializeField] Material hologramMaterial;

    public bool Usable { get => usable; }
    public ObstacleType Type { get => type; }
    public float Speed { get => speed; }

    private void Awake()
    {
        initialTransform = transform;
    }

    private void Start()
    {
        _defaultMaterials = new Material[_renderer.materials.Length];
        for (int i = 0; i < _renderer.materials.Length; i++)
        {
            _defaultMaterials[i] = _renderer.materials[i];
        }
    }

    private void OnEnable()
    {
        EnableColliders();

        transform.position = initialTransform.position;
        transform.rotation = initialTransform.rotation;
    }

    private void FixedUpdate()
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
        if (speed == 0)
        {
            Debug.LogWarning("Error assigning new speed");
        }
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
            ActivateHologram();
            DisableColliders();
        }
    }

    private void ActivateHologram()
    {
        Material[] newMaterials = new Material[_renderer.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = hologramMaterial;
        }

        _renderer.materials = newMaterials;
    }

    private void OnDisable()
    {
        EnableColliders();
        _renderer.materials = _defaultMaterials;
    }
}
