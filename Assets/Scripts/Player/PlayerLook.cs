using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity;
    [SerializeField] Transform playerBody;

    private float xRotation = 0f; // Track pitch
    private Vector2 mouseInput;

    private void Start()
    {
        InputManager.Instance.onLookChanged += OnLookChanged;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnLookChanged(Vector2 _mouseInput)
    {
        mouseInput = _mouseInput;
    }

    private void Update()
    {
        // Calculate the mouse input for look sensitivity and delta time
        Vector2 input = mouseInput * lookSensitivity * Time.deltaTime;

        // Update xRotation based on mouse input, clamped to avoid flipping
        xRotation -= input.y;
        xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f);

        // Apply rotations using Quaternion
        Quaternion rotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = rotation; // Apply the rotation to the camera

        // Rotate the player body around the Y axis (yaw)
        playerBody.Rotate(Vector3.up * input.x);
    }
}