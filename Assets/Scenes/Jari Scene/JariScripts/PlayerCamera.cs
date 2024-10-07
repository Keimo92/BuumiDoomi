using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float SensitivityX;
    public float SensitivityY;

    public Transform Orientation;

    float XRot;
    float YRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get the inputs
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;

        YRot += mouseX;
        XRot -= mouseY;

        //Clamp Values of your XRotation
        XRot = Mathf.Clamp(XRot, -90, 90);

        //Rotate camera and Orientatione
        transform.rotation = Quaternion.Euler(XRot, YRot, 0);
        Orientation.rotation = Quaternion.Euler(0, YRot, 0);

    }
}
