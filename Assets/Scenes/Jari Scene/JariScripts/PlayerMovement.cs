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

    public float JumpForce;
    public float JumpCooldown;
    public float AirMultiplier;
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
        //GroundCheck
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, GroundedMask);
        
        //Make drag to player
        if (IsGrounded )
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
    }

    private void PlayerInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        //Check Jumping
        if(Input.GetKey(JumpKey) && ReadyToJump && IsGrounded )
        {
            ReadyToJump = false;
            Jump();

            Invoke(nameof(ResetJump),JumpCooldown);
        }
    }

    private void PlayerMove()
    {
        MoveDir = Orientation.forward * VerticalInput + Orientation.right * HorizontalInput;
        
        //On ground
        if (IsGrounded )
        {
           Rigidbody.AddForce(MoveDir.normalized * MovementSpeed * 10f, ForceMode.Force);
        }
        
        
        //Air 
        else if(!IsGrounded )
        {
          Rigidbody.AddForce(MoveDir.normalized * MovementSpeed * 10f * AirMultiplier, ForceMode.Force);
        }
        
        
       
    }

    private void SpeedControl()
    {
        Vector3 FlatVelocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
        // Limit the speed
        if ( FlatVelocity.magnitude > MovementSpeed )
        {
            Vector3 limitedVel = FlatVelocity.normalized * MovementSpeed;
            Rigidbody.velocity = new Vector3(limitedVel.x, Rigidbody.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Reset y velocity!!
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0f, Rigidbody.velocity.z);
       

        Rigidbody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        Debug.Log("Player is Jumping");
    }

    private void ResetJump()
    {
        ReadyToJump = true;
        
    }
}
