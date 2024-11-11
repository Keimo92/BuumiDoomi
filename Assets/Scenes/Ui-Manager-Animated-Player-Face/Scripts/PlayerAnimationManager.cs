using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerAnimationManager : MonoBehaviour
{
    Animator animator;
    public static event Action <PlayerFaceState> PlayerAnimationChanged;

    public static PlayerAnimationManager instance;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if ( instance != null && instance != this )
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public enum PlayerFaceState
    {
        Idle,
        Hurt,
        PickUp
    }

    public PlayerFaceState currentState;


    public void SetPlayerFaceState(PlayerFaceState newFaceState)
    {
        if ( currentState == newFaceState) return;

        currentState = newFaceState;

        PlayerAnimationChanged?.Invoke(currentState);
        switch (newFaceState)
        {
            case PlayerFaceState.Idle:
                PlayerIdle();
                break;
            case PlayerFaceState.PickUp:
                StartCoroutine(FaceRoutineForPickUp());
                break;
            case PlayerFaceState.Hurt:
                Playerhurt();
                break;
        }
    }

    void Playerhurt()
    {
        animator.SetTrigger("Hurt");
    }

    void PlayerIdle()
    {
        animator.SetTrigger("Idle");
    }

    void PlayerPickUp()
    {
        animator.SetTrigger("Pickup");
    }

    //Fixing this later
    IEnumerator FaceRoutineForPickUp()
    {
        PlayerPickUp();
        yield return new WaitForSeconds(1.5f);
        
    }
}

