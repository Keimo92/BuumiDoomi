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

    private void Start()
    {
        // This finds the components in the scene from gameobjects. Its not necessary to assing them!
        DialogueManager = FindAnyObjectByType<DialogueManager>();
        ScreenFlash = FindAnyObjectByType<ScreenFlash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity) )
        {
            if ( entity.CurrentHealth < 100 )
            {
                entity.AddHealth(Healthpack.healingAmount);
                DialogueManager.StartHealthPackInfo(Healthpack);
                StartCoroutine(ScreenFlash.SetColorAlpha());
                Destroy(gameObject, 0.3f);

            }

        }
    }
}