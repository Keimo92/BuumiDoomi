using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamShakeTest : MonoBehaviour
{
   public ICameraShaker Shaker;

    private void Awake()
    {
        Shaker = FindObjectOfType<CameraController>();
    }

    void TakeDamage()
    {
        if ( Shaker != null )
        {
            Shaker.ShakeCamera(4, 0.4f);
            Debug.Log("Cam Shaking");
        }
    }


    private void Update()
    {
        if  ( Input.GetKeyDown(KeyCode.K) )
        {
            TakeDamage();
        }
    }
}
