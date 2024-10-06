using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour, ICameraShaker
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;
    private float shakeTimer;

    void Start()
    {
       
        cinemachinePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    // Implementation of the ShakeCamera method from ICameraShake
    public void ShakeCamera(float intensity, float duration)
    {
        cinemachinePerlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    void Update()
    {
        //TESTING
        if ( shakeTimer > 0 )
        {
            shakeTimer -= Time.deltaTime;
            if ( shakeTimer <= 0f )
            {
                // Reset the shake
                cinemachinePerlin.m_AmplitudeGain = 0f;
            }
        }
    }

    private void ResetIntensity()
    {
        cinemachinePerlin.m_AmplitudeGain = 0f;
    }
}
