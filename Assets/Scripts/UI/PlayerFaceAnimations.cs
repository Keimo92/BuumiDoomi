using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;
using System;

public class PlayerFaceAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator FaceRoutineForPickUp()
    {
        animator.SetTrigger("Pickup");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Idle");
    }

    public void OnPlayerTakeDamage()
    {
        animator.SetTrigger("Hurt");
    }

    public void OnPickupablePickedUp()
    {
        StartCoroutine(FaceRoutineForPickUp());
    }

}
