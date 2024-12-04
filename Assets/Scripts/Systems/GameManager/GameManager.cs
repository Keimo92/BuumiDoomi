using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float timeToReloadScene;
    [SerializeField] private TextMeshProUGUI collectiblesText;
    [SerializeField] private GameObject collectibleObj; //Place holder which will be set to false after the level finish.
    public List<string> sceneNames; // Scene assets did not work after builded the game. If this string array solution is not good. Lets fix it, for now this should do that we can track what scenes are in the inspector.
    public static event Action OnLevelLoaded;
    [SerializeField] private float collectibleTextShowTime = 8f;

    public bool onLevelFinished = false;
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
        if ( collectibleObj != null )
        {
            collectibleObj.SetActive(false);
        }
        currentState = GameState.Playing;
        OnLevelLoaded?.Invoke(); // This can be removed when we have main menu
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

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        OnLevelLoaded?.Invoke();
        collectibleObj = GameObject.Find("CollectiblePlaceHolder");
        collectiblesText = GameObject.Find("CollectibleText")?.GetComponent<TextMeshProUGUI>();
        collectibleObj.SetActive(false);
        SetGameState(GameState.Playing);
        FindObjectOfType<PlayerEntity>().transform.position = spawnPos; //Move player to the current spawn position
        asyncOperation.completed -= OnSceneLoaded;
    }


    //When we exit the level this is called;
    private IEnumerator WaitAndLoadNextLevel()
    {
        if ( collectibleObj != null )
        {
            collectibleObj.SetActive(true);

            int collectiblesLeft = CollectibleDataPersistence.instance.GetCollectiblesLeft();
            if ( collectiblesText != null )
            {
                collectiblesText.text = $"You missed total of : {collectiblesLeft} collectibles";
            }
            onLevelFinished = true;
            Debug.Log(onLevelFinished);
            yield return new WaitForSeconds(collectibleTextShowTime);
        }
        onLevelFinished = false;
        yield return new WaitForSeconds(timeToReloadScene);
        if ( currentLevelIndex < sceneNames.Count - 1 )
        {
            currentLevelIndex++;
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneNames[currentLevelIndex]);
            loadOperation.completed += OnSceneLoaded;
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
        int collectiblesLeft = CollectibleDataPersistence.instance.GetCollectiblesLeft();
        collectiblesText.text = $"You missed total of : {collectiblesLeft} collectibles";
        collectibleObj.SetActive(true);
        yield return new WaitForSeconds(timeToReloadScene);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        loadOperation.completed += OnSceneLoaded;
    }

    private void OnPausePressed()
    {
        GameManager.Instance.TogglePauseGame();
    }

    public void OnCheckpointReached(Component sender, object data)
    {
        //Update spawnpos when checkpoint has been reached
        if ( data is GameEventData.OnCheckpointReached eventData )
        {
            spawnPos = eventData.position;
        }
    }
}