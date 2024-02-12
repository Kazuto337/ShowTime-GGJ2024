using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Collider myCollider;
    [SerializeField] float respawn = 30f;
    Rigidbody[] rigibodies;
    bool bIsRagDoll = false;
    private FMOD.Studio.EventInstance foosteps;

    //movement
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    private float verticalVelocity;
    private float groundedTimer;        // to allow jumping when going down ramps

    [Header("To set different Player Speed")]
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float reduceSpeedFactor = 0.2f;


    [Header("To set different backguard velocities")]
    [SerializeField] private float bckdMoveNormal = 1.0f;
    [SerializeField] private float bckdMoveRagDoll;
    [SerializeField] private float bckdMove = 1.0f;
    [Space(2)]
    [Header("To set different behaviors")]
    private bool isDrunkWalk = false;
    private bool canMove = true;
    public bool isWalking = false;

    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    [SerializeField] EventReference FootstepEvent;
    [SerializeField] EventReference hitEvent;
    [SerializeField] EventReference laughEvent;
    [SerializeField] EventReference endGameEvent;
    [SerializeField] float rate;
    [SerializeField] GameObject player;
    float time;

    private void Start()
    {
        rigibodies = GetComponentsInChildren<Rigidbody>();
        ToggleRaddoll(true);


    }
    private void Update()
    {
        bckdMoveRagDoll = ConveyerBelt.speed;

        if (Input.GetKey("v")) ToggleRaddoll(false);
        if (Input.GetKey("c")) ToggleRaddoll(true);

        #region checking ground
        //fmod
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer) groundedTimer = 0.2f;
        if (groundedTimer > 0) groundedTimer -= Time.deltaTime;
        if (groundedPlayer && verticalVelocity < 0) verticalVelocity = 0f;
        #endregion
        verticalVelocity -= gravityValue * Time.deltaTime;

        #region movement parameters
        Vector3 move;
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");

        if (vMovement != 0 || hMovement != 0)
        {
            isWalking = true;
        }
        else isWalking = false;

        time += Time.deltaTime;
        if (isWalking && canMove)
        {
            if (time >= rate)
            {
                PlayFootStep();
                time = 0;
            }
        }

        if (vMovement > 0 && canMove)
        {
            //fmod walk
            

            move = new Vector3(hMovement, 0, -vMovement);
            animator.SetFloat("ZSpeed", vMovement);
            animator.SetBool("Drunk", isDrunkWalk);
            animator.SetFloat("XSpeed", Mathf.Abs(hMovement));

        }
        else if (groundedTimer > 0)
        {
            move = new Vector3(hMovement, 0, bckdMove);
            //quieto
            animator.SetFloat("ZSpeed", 0);
            animator.SetFloat("XSpeed", Mathf.Abs(hMovement));
        }
        else
        {
            //quieto
            move = new Vector3(0, 0, 0);
            animator.SetFloat("ZSpeed", 0);
            animator.SetFloat("XSpeed", 0);
        }


        move *= playerSpeed;
        #endregion


        if (move.magnitude > 0.05f)
        {
            //gameObject.transform.forward = move;
        }

        // allow jump as long as the player is on the ground
        if (Input.GetButtonDown("Jump") && canMove)
        {
            // must have been grounded recently to allow jump
            if (groundedTimer > 0)
            {
                // no more until we recontact ground
                groundedTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                verticalVelocity += Mathf.Sqrt(jumpHeight * 2 * gravityValue);

            }
        }

        // inject Y velocity before we use it
        move.y = verticalVelocity;
        animator.SetFloat("Jump", move.y);

        // call .Move() once only
        controller.Move(move * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Obstacle(Wide)" || hit.gameObject.tag == "Obstacle(Tall)" || hit.gameObject.CompareTag("ThrowableObject"))
        {
            //Debug.LogError("Big");
            GameEvents.instance.OnPlayerHitted.Invoke();
            //hit.gameObject.GetComponent<Obstacle>().DisableColliders();

            FMODUnity.RuntimeManager.PlayOneShot(hitEvent, transform.position);
            FMODUnity.RuntimeManager.PlayOneShot(laughEvent, transform.position);
            //fmod major hit
        }
        if (hit.gameObject.tag == "Obstacle(Small)")
        {            
            isDrunkWalk = true;
            playerSpeed -= reduceSpeedFactor;
            Debug.LogError("Small");
            FMODUnity.RuntimeManager.PlayOneShot(hitEvent, transform.position);
            FMODUnity.RuntimeManager.PlayOneShot(laughEvent, transform.position);

            // fmod minor hit
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeadZone")
        {
            EndGame();
            FMODUnity.RuntimeManager.PlayOneShot(endGameEvent, transform.position);
            //fmod swamp
        }
    }

    public void ToggleRaddoll(bool bisAnimating)
    {
        canMove = bisAnimating;
        bIsRagDoll = !bIsRagDoll;

        controller.enabled = bisAnimating;
        foreach (Rigidbody ragdollBone in rigibodies)
        {
            ragdollBone.isKinematic = bisAnimating;
        }


        animator.enabled = bisAnimating;
        if (bisAnimating)
        {
            //animator.SetTrigger("Run");

        }

        if (bisAnimating == false) bckdMove = bckdMoveRagDoll;
        else bckdMove = bckdMoveNormal;
        Invoke("ReactivateCharacterController", .6f);
    }

    public void PlayFootStep()
    {
        RuntimeManager.PlayOneShotAttached(FootstepEvent, player);
    }

    private void ReactivateCharacterController()
    {
        controller.enabled = true;
    }
    private void EndGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot(endGameEvent, transform.position);
        GameManager.instance.GameOver();
        Debug.LogError("Game has ended");
    }

}