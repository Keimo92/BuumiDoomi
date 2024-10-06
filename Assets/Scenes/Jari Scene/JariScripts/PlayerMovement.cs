using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerMovement")]
    public float MovementSpeed;

    public Transform Orientation;

    [Header("Grounded")]
    public LayerMask GroundedMask;
    public float PlayerHeight;
    public float GroundDrag;
    bool IsGrounded;

    [Header("Jump Variables")]
    public float FallMultiplier;
    public float JumpForce; 
    public float JumpCooldown;
    public float AirMultiplier;
    public float MaxJumpHeight; 
    bool ReadyToJump;

    [Header("KeyBinds")]
    public KeyCode JumpKey = KeyCode.Space;

    float HorizontalInput;
    float VerticalInput;

    Vector3 MoveDir;

    Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.freezeRotation = true;
        ReadyToJump = true;
    }

    private void Update()
    {
        // GroundCheck
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, GroundedMask);

        // Apply drag when grounded
        if ( IsGrounded )
        {
            Rigidbody.drag = GroundDrag;
        }
        else
        {
            Rigidbody.drag = 0f;
        }

        PlayerInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        PlayerMove();

        // Apply the FallMultiplier
        if ( Rigidbody.velocity.y < 0 )
        {
            Rigidbody.velocity += Vector3.up * Physics.gravity.y * FallMultiplier * Time.deltaTime;
        }

        // Calling the methods from update
        ClampHorizontalVelocity();
        ClampJumpHeight();
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        // Jump when pressing the space bar and ready to jump
        if ( Input.GetKeyDown(JumpKey) && ReadyToJump && IsGrounded )
        {
            ReadyToJump = false;
            Jump();

            Invoke(nameof(ResetJump), JumpCooldown); //Cooldown for next jump
        }
    }

    private void PlayerMove()
    {
        MoveDir = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;

        if ( IsGrounded )
        {
            Rigidbody.AddForce(MoveDir.normalized * MovementSpeed * 10f, ForceMode.Force);
        }
        else if ( !IsGrounded )
        {
            Rigidbody.AddForce(MoveDir.normalized * MovementSpeed * 10f * AirMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 FlatVelocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

        if ( FlatVelocity.magnitude > MovementSpeed )
        {
            Vector3 limitedVel = FlatVelocity.normalized * MovementSpeed;
            Rigidbody.velocity = new Vector3(limitedVel.x, Rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);

        Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);

        Debug.Log("Player is Jumping");
    }

    private void ResetJump()
    {
        ReadyToJump = true;
    }

    //Horizontal Clamping
    private void ClampHorizontalVelocity()
    {
        if ( !IsGrounded )
        {
            float clampedX = Mathf.Clamp(Rigidbody.velocity.x, -MovementSpeed, MovementSpeed);
            float clampedZ = Mathf.Clamp(Rigidbody.velocity.z, -MovementSpeed, MovementSpeed);

            Rigidbody.velocity = new Vector3(clampedX, Rigidbody.velocity.y, clampedZ);

        }
    }

    // Using Clamp to ensure the player doesn't exceed MaxJumpHeight
    private void ClampJumpHeight()
    {
        if ( transform.position.y >= MaxJumpHeight )
        {
            // When reaching MaxJumpHeight, clamp the vertical velocity
            float clampedY = Mathf.Clamp(Rigidbody.velocity.y, float.NegativeInfinity, 0f);
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, clampedY, Rigidbody.velocity.z);
        }
    }
}