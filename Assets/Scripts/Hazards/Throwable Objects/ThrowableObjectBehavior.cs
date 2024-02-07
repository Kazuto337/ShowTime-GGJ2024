using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObjectBehavior : MonoBehaviour
{
    [Header("Object Data") , Space(25f)]
    [SerializeField] ThrowableObject objectData;

    [Header("Movement Properties")]
    Vector3 initialPosition;
    Rigidbody rgdb;
    float verticalSpeed, throwingTime, horizontalSpeed;
    [SerializeField] bool canThrow;
    bool onFloor;

    Animator animator;

    public float ThrowingTime { get => throwingTime;}
    public bool CanThrow { get => canThrow; }    

    private void Awake()
    {
        animator = GetComponent<Animator>();

        verticalSpeed = objectData.VerticalSpeed;
        UpdateSpeed();

        rgdb = GetComponent<Rigidbody>();

        initialPosition = transform.position;
    }
    private void Start()
    {
        canThrow = false;
    }

    private void OnEnable()
    {
        animator.Play("IdleThrowableObject");
    }

    private void Update()
    {
        if (onFloor)
        {
            Vector3 currentPosition = transform.position;
            float newZAxisPosition = currentPosition.z + horizontalSpeed * Time.deltaTime;

            transform.position = new Vector3(currentPosition.x, currentPosition.y, newZAxisPosition);
        }
    }

    public void UpdateSpeed()
    {
        horizontalSpeed = ConveyerBelt.speed;
    }

    /// <summary>
    /// Modify Throwable Status
    /// </summary>
    /// <param name="newValue"></param>
    public void ModifyThrowStatus(bool newValue)
    {
        canThrow = newValue;
    }

    [ContextMenu("Throw Object")]
    public void ThrowObject()
    {
        Vector2 playerPosition = GameManager.instance.GetPlayerPosition();
        Vector3 droppingZonePosition = new Vector3(playerPosition.x, 10, playerPosition.y);

        gameObject.SetActive(true);
        canThrow = false;

        StartCoroutine(ThrowObjectBehavior(droppingZonePosition));
    }

    private IEnumerator ThrowObjectBehavior(Vector3 dropingZonePosition)
    {
        float t = 0;
        while (transform.position != dropingZonePosition)
        {
            t += Time.deltaTime * verticalSpeed;
            transform.position = Vector3.Lerp(transform.position, dropingZonePosition, Mathf.Clamp(t, 0, 1));

            if (Mathf.Abs(transform.position.magnitude - dropingZonePosition.magnitude) < 0.5)
            {
                transform.localPosition = dropingZonePosition;
                Debug.Log("ThrowableObject postion = " + transform.position);
                break;
            }

            yield return null;
        }
        Debug.Log(Mathf.Clamp(t, 0, 1));
        Debug.Log("(Outside) ThrowableObject postion = " + transform.position);
        DropObject();

        yield return null;
    }

    public void DropObject()
    {
        rgdb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Throwable on Floor");
            onFloor = true;
            animator.Play("Static");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            gameObject.SetActive(false);
            ResetProperties();
        }
    }

    private void OnDisable()
    {
        canThrow = true;
        onFloor = false;
        ResetProperties();
    }

    public void ResetProperties()
    {
        transform.position = initialPosition;
        canThrow = true;
        rgdb.useGravity = false;
    }

    public ThrowableObjectType GetObjectType()
    {
        return objectData.Type;
    }
}
