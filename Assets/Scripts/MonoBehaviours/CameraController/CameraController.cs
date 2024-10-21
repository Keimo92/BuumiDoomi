using System.Collections;
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
        cinemachinePerlin.m_AmplitudeGain = 0f;
    }

    // Implementation of the ShakeCamera method from ICameraShake
    public void ShakeCamera(float intensity, float duration)
    {
        cinemachinePerlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
        StartCoroutine(ResetShakeCam());

    }


    public IEnumerator ResetShakeCam()
    {
        Debug.Log("Coroutine starts");
        yield return new WaitForSeconds(shakeTimer);
        cinemachinePerlin.m_AmplitudeGain = 0f;
    }
}
