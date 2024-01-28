using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Collider myCollider;
    [SerializeField] float respawn = 30f;
    Rigidbody[] rigibodies;
    bool bIsRagDoll = false;

    //movement
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator animator;
    private float verticalVelocity;
    private float groundedTimer;        // to allow jumping when going down ramps
    private float playerSpeed = 2.0f;

    [Header("To set different backguard velocities")]
    [SerializeField] private float bckdMove = 1.0f;
    [SerializeField] private float bckdMoveNormal = 1.0f;
    [SerializeField] private float bckdMoveRagDoll = 2.0f;
    [Space(2)]

    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f;

    private void Start()
    {
        rigibodies = GetComponentsInChildren<Rigidbody>();
        ToggleRaddoll(true);

    }
    private void Update()
    {
        if (Input.GetKey("v")) ToggleRaddoll(false);
        if (Input.GetKey("c")) ToggleRaddoll(true);

        #region checking ground
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
        Debug.Log(vMovement);

        if (vMovement > 0)
        {
            move = new Vector3(hMovement, 0, -vMovement);
            animator.SetFloat("ZSpeed", vMovement);
            animator.SetFloat("XSpeed", Mathf.Abs(hMovement));

        }
        else if (groundedTimer > 0)
        {
            move = new Vector3(hMovement, 0, bckdMove);
           
            animator.SetFloat("ZSpeed", 0);
            animator.SetFloat("XSpeed", Mathf.Abs(hMovement));
        }
        else
        {
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
        if (Input.GetButtonDown("Jump"))
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

    private void ToggleRaddoll(bool bisAnimating)
    {
        Debug.Log("Toggle Radboll");
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
            Debug.Log("Toggle RadbollIs runing I guess");

        }

        if (bisAnimating == false) bckdMove = bckdMoveRagDoll;
        else bckdMove = bckdMoveNormal;
        Invoke("ReactivateCharacterController", .6f);
    }

    private void ReactivateCharacterController()
    {
        controller.enabled = true;
    }

}