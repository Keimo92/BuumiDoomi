using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Input Actions
    public InputAction exampleAction;
    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;

    //Delegates
    public delegate void ExampleActionPressed();
    public delegate void OnMoveChanged(Vector2 moveInput);
    public delegate void OnLookChanged(Vector2 lookInput);
    public delegate void OnJumpPressed();

    public ExampleActionPressed exampleActionPressed;
    public OnMoveChanged onMoveChanged;
    public OnLookChanged onLookChanged;
    public OnJumpPressed onJumpPressed;

    //Singleton
    public static InputManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        exampleAction.Enable();
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
    }

    private void Update()
    {
        //Example
        if (exampleAction.WasPressedThisFrame())
        {
            Debug.Log("Example Input Pressed This Frame");
            exampleActionPressed?.Invoke();
        }

        //
        //Input Event Invoking
        //
        onLookChanged?.Invoke(lookAction.ReadValue<Vector2>());     
        onMoveChanged?.Invoke(moveAction.ReadValue<Vector2>());
        if(jumpAction.WasPressedThisFrame()) onJumpPressed?.Invoke();
    }
}
