using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasperPlayerLook : MonoBehaviour
{
    [SerializeField] float lookSensitivity;
    [SerializeField] Transform playerBody;

    Vector2 mouseInput;
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
        Vector2 input = mouseInput * lookSensitivity * Time.deltaTime;
        float xRotation = -input.y;
        xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f);
        transform.eulerAngles += new Vector3(xRotation, 0, 0);
        playerBody.eulerAngles += new Vector3(0, input.x, 0);
    }
}
