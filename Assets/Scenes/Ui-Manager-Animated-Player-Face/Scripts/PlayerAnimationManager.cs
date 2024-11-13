using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XInput;
using System;

public class PlayerAnimationManager : MonoBehaviour
{
    public static event Action<PlayerFaceState> OnPlayerFaceChange; 

    public enum PlayerFaceState
    {
        Idle,
        Hurt,
        PickUp
    }

    public PlayerFaceState currentState;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public static void ChangeFaceState(PlayerFaceState newFaceState)
    {
        OnPlayerFaceChange?.Invoke(newFaceState);
    }
    private void OnEnable()
    {
        OnPlayerFaceChange += SetPlayerFaceState;
    }

    private void OnDisable()
    {
        OnPlayerFaceChange -= SetPlayerFaceState;
    }

    public void SetPlayerFaceState(PlayerFaceState newFaceState)
    {
        currentState = newFaceState;

        switch ( newFaceState )
        {
            case PlayerFaceState.Idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerFaceState.Hurt:
                animator.SetTrigger("Hurt");
                break;
            case PlayerFaceState.PickUp:
                StartCoroutine(FaceRoutineForPickUp());
                break;
        }
    }

    private IEnumerator FaceRoutineForPickUp()
    {
        animator.SetTrigger("Pickup");
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Idle");
    }
}
