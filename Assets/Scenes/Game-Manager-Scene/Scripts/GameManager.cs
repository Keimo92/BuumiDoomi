using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        LoadNextLevel
    }

    private bool isPaused;

    public static GameManager Instance { get; private set; }
    private GameState currentState;

    public static event Action<GameState> OnGameStateChanged;

    private int currentLevelIndex = 0;
    private int totalLevels = 2; // How many levels we have

    private void Awake()
    {
        if ( Instance != null && Instance != this )
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        currentState = GameState.Playing;
    }

    //Setting the GameState here. GameManager.Instance.SetGameState(GameState.LoadNextLevel); Or if Dying GameManager.Instance.SetGameState(GameState.GameOver); You got the point. <3
    public void SetGameState(GameState newState)
    {
        if ( currentState == newState ) return;

        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);

        switch ( newState )
        {
            case GameState.LoadNextLevel:
                LoadNextLevel();
                break;

            case GameState.GameOver:
                ReturnToLevel();
                break;

            case GameState.Paused:
                PauseGame();
                break;
            case GameState.Playing:
                UnpauseGame();
                break;
        }
    }

    private void OnLevelLoaded(AsyncOperation asyncOperation)
    {
        SetGameState(GameState.Playing);
        asyncOperation.completed -= OnLevelLoaded;
    }

    public void LoadNextLevel()
    {
        if ( currentLevelIndex < totalLevels ) 
        {
            currentLevelIndex++;
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentLevelIndex);
            loadOperation.completed += OnLevelLoaded;
        }
        else
        {
            Debug.Log("No more levels to load. Game Over or restart game.");
            //GameState MainMenu?
            SetGameState(GameState.GameOver); 
        }
    }

    public void ReturnToLevel()
    {
        StartCoroutine(WaitForLoadingScene());
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
        Debug.Log("Game Paused");
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    private IEnumerator WaitForLoadingScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        loadOperation.completed += OnLevelLoaded;
    }
}
