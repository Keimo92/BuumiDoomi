using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Input Actions
    public InputAction exampleAction;

    //Delegates
    public delegate void ExampleActionPressed();
    public ExampleActionPressed exampleActionPressed;

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
    }

    private void Update()
    {
        if (exampleAction.WasPressedThisFrame())
        {
            Debug.Log("Example Input Pressed This Frame");
            exampleActionPressed?.Invoke();
        }
        
    }
}
