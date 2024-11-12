using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectiblesText;
    [SerializeField] private GameObject collectibleObj; //Place holder which will be set to false after the level finish.
    public List<string> sceneNames; // Scene assets did not work after builded the game. If this string array solution is not good. Lets fix it, for now this should do that we can track what scenes are in the inspector.
    [SerializeField] private float timeToReloadScene;
    [SerializeField] private float collectibleTextShowTime = 8f;

    public static event System.Action OnLevelLoaded;
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


    private void Start()
    {
        collectibleObj.SetActive(false);
        OnLevelLoaded?.Invoke();
    }
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

    private void Update()
    {

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
    private void OnLevelLoadedHandler(AsyncOperation asyncOperation)
    {
        SetGameState(GameState.Playing);
        asyncOperation.completed -= OnLevelLoadedHandler;
        OnLevelLoaded?.Invoke();

        collectibleObj = GameObject.Find("CollectiblePlaceHolder");
        collectiblesText = GameObject.Find("CollectibleText")?.GetComponent<TextMeshProUGUI>();
        collectibleObj.SetActive(false);
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

            yield return new WaitForSeconds(collectibleTextShowTime);
        }

        yield return new WaitForSeconds(timeToReloadScene);

        if ( currentLevelIndex < sceneNames.Count - 1 )
        {
            currentLevelIndex++;
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneNames[currentLevelIndex]);
            loadOperation.completed += OnLevelLoadedHandler;
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
        loadOperation.completed += OnLevelLoadedHandler;
    }

    private void OnPausePressed()
    {
        GameManager.Instance.TogglePauseGame();
    }
}


