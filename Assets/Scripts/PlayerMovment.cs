using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public CharacterController playerController;

    public Transform camer;
    public Animator animator;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    //movment
    public float walkSpeed;
    public float sprintSpeed;
    private float trueSpeed;
    private bool sprinting;
    //jump
    public float jumpingHeight;
    public float gravity;
    bool isOnGround;
    Vector3 velocity;

    private void Start()
    {
        trueSpeed = walkSpeed;
    }

    void Update() 
    {
        UnLockCursor();
        animator.SetBool("IsOnGround", isOnGround);
        isOnGround = Physics.CheckSphere(transform.position, .1f, 1);
        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -1;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
            sprinting = false;
        }

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camer.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerController.Move(moveDir.normalized * trueSpeed * Time.deltaTime);

            if (sprinting)
            {
                animator.SetFloat("Speed", 2f);
            }
            else
            {
                animator.SetFloat("Speed", 1f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        if (Input.GetButton("Jump") && isOnGround)
        {
            velocity.y = Mathf.Sqrt((jumpingHeight * 10) * -2f * gravity);
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        if (velocity.y > -20)
        {
            velocity.y += (gravity * 10) * Time.deltaTime;
        }
        playerController.Move(velocity * Time.deltaTime);
    }

    public void UnLockCursor()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}