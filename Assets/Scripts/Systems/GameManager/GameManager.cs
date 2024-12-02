using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeToReloadScene;
    public List<string> sceneNames; // Scene assets did not work after builded the game. If this string array solution is not good. Lets fix it, for now this should do that we can track what scenes are in the inspector.

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        LoadNextLevel
    }

    public static GameManager Instance { get; private set; }
    private GameState currentState;

    public static event Action<GameState> OnGameStateChanged;

    private int currentLevelIndex;

    private void Awake()
    {
        if ( Instance != null && Instance != this )
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentState = GameState.Playing;
    }

    //Set the game state here from other classes
    public void SetGameState(GameState newState)
    {
        if ( currentState == newState ) return;

        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);

        switch ( newState )
        {
            case GameState.LoadNextLevel:
                StartCoroutine(WaitAndLoadNextLevel());
                break;

            case GameState.GameOver:
                StartCoroutine(ReturnToLevelAfterDeathRoutine());
                break;

            case GameState.Paused:
                PauseGame();
                break;

            case GameState.Playing:
                UnpauseGame();
                break;
        }

        Debug.Log(currentState);
    }

    private void OnEnable()
    {
        InputManager.Instance.onPauseActionPressed += OnPausePressed;
    }

    private void OnDisable()
    {
        InputManager.Instance.onPauseActionPressed -= OnPausePressed;
    }

    private void OnLevelLoaded(AsyncOperation asyncOperation)
    {
        SetGameState(GameState.Playing);
        asyncOperation.completed -= OnLevelLoaded;
    }


    //When we exit the level this is called;
    private IEnumerator WaitAndLoadNextLevel()
    {
        yield return new WaitForSeconds(timeToReloadScene);
        if ( currentLevelIndex < sceneNames.Count - 1 )
        {
            currentLevelIndex++;
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneNames[currentLevelIndex]);
            loadOperation.completed += OnLevelLoaded;
        }
        else
        {
            Debug.Log("No more levels to load. Game Over or restart game.");
            SetGameState(GameState.GameOver);
        }
    }
    public GameState GetCurrentState()
    {
        return currentState;
    }

    public void TogglePauseGame()
    {
        if ( currentState == GameState.Playing )
        {
            SetGameState(GameState.Paused);
        }
        else if ( currentState == GameState.Paused )
        {
            SetGameState(GameState.Playing);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    //Return the current scene after player is dead
    private IEnumerator ReturnToLevelAfterDeathRoutine()
    {
        yield return new WaitForSeconds(timeToReloadScene);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        loadOperation.completed += OnLevelLoaded;
    }

    private void OnPausePressed()
    {
        GameManager.Instance.TogglePauseGame();
    }
}