using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CasperPlayerMovement : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] CharacterController controller;

    [Header("Horizontal Movement Settings")]
    [SerializeField] float maxHorizontalVelocity;
    [SerializeField] float horizontalAccelerationTime;

    [Header("Vertical Movement Settings")]
    [SerializeField] float playerGravity;
    [SerializeField] float maxVerticalVelocity;
    [SerializeField] float jumpForce;

    [Header("Ground Check Settings")]
    [SerializeField] Transform groundCheckPosition;
    [SerializeField] float groundCheckLength;
    [SerializeField] LayerMask groundLayerMask;

    [Header("Debug")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector3 velocity;
    [SerializeField] Vector3 horizontalVelocity;
    [SerializeField] float verticalVelocity;
    [SerializeField] bool jumpPressed;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    

    private void Start()
    {
        InputManager.Instance.onMoveChanged += OnMoveChanged;
        InputManager.Instance.onJumpPressed += OnJumpPressed;

        controller = GetComponent<CharacterController>();
    }

    private void OnMoveChanged(Vector2 _moveInput)
    {
        moveInput = _moveInput;
    }

    private void OnJumpPressed()
    {
        if(isGrounded) jumpPressed = true;
    }

    private void Update()
    {
        GroundCheck();
        CalculateHorizontalVelocity();
        CalculateVerticalVelocity();

        velocity = horizontalVelocity;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CalculateHorizontalVelocity()
    {
        //Calculate acceleration rate
        float horizontalAccelerationRate = maxHorizontalVelocity / horizontalAccelerationTime;

        //Horizontal movement with acceleration
        Vector3 forward = transform.forward * moveInput.y;
        Vector3 right = transform.right * moveInput.x;
        Vector3 targetVelocity = (forward + right).normalized * maxHorizontalVelocity; //Calculate target velocity
        Vector3 acceleration = (targetVelocity - horizontalVelocity) * horizontalAccelerationRate * Time.deltaTime; //Acceleration formula. Acceleration = Change in velocity/Time
        horizontalVelocity += acceleration; //Add acceleration to the velocity
        horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, maxHorizontalVelocity); //Clamp to maxSpeed
    }

    private void CalculateVerticalVelocity()
    {
        //
        //EXIT STATEMENTS
        //
        if (jumpPressed) //If jump pressed we set velocity to jumpForce. We do not accelerate
        {
            jumpPressed = false;
            isJumping = true;
            verticalVelocity = jumpForce;
            return;
        } else if(isGrounded && !isJumping) //If at ground. Vertical velocity 0
        {
            verticalVelocity = 0f;
            return;
        } else //Else. We add gravity.
        {   
            verticalVelocity -= playerGravity * Time.deltaTime;
            verticalVelocity = Mathf.Clamp(verticalVelocity, -maxVerticalVelocity, maxVerticalVelocity); //Clamp to max
        }
    }

    private void GroundCheck()
    {
        if(Physics.Raycast(groundCheckPosition.position, Vector3.down, groundCheckLength, groundLayerMask))
        {
            if (isJumping && !isGrounded) //We have landed after jumping
            {
                isGrounded = true;
                isJumping = false;
            }
            else
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isGrounded)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        if(groundCheckPosition != null) Gizmos.DrawRay(groundCheckPosition.position, Vector3.down * groundCheckLength);
    }
        
}
