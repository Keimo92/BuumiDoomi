using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;

public class HealthPickUp : MonoBehaviour
{
    public HealthPack Healthpack;

    public DialogueManager DialogueManager;

    public ScreenFlash ScreenFlash;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity entity))
        {
            entity.AddHealth(Healthpack.healingAmount);
            DialogueManager.StartHealthPackInfo(Healthpack);
            StartCoroutine(ScreenFlash.SetColorAlpha());
            Destroy(gameObject, 0.3f);

        }
    }
}