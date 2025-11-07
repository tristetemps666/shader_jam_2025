using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    InGame,
    InPause,
    InWin,
};

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public UnityEvent OnGameUnPaused = new();
    public UnityEvent OnGamePaused = new();
    public UnityEvent OnGameStarted = new();

    public int ammountOfCoinToWin => _valueForTheWin;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private AgentBehavior _playerBehavior;
    private NavMeshAgent _playerNavMeshAgent;

    private PlayerCollectibleManager _playerCollectible;

    [SerializeField]
    private int _valueForTheWin = 50;

    private float _initialPlayerSpeed = 0f;

    [SerializeField, ReadOnly(true)]
    private GameState _gameState;

    [SerializeField]
    private InputActionMap _uiInputs;

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_STANDALONE
        Application.Quit();

#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        _uiInputs["UnPause"].Enable();

        _playerBehavior.enabled = false;
        _playerNavMeshAgent.speed = 0f;

        _uiManager.SetPauseUI();

        _gameState = GameState.InPause;

        OnGamePaused.Invoke();
    }

    public void UnPauseGame()
    {
        _uiInputs["UnPause"].Disable();

        _playerBehavior.enabled = true;
        _playerNavMeshAgent.speed = _initialPlayerSpeed;

        _uiManager.SetGameUI();

        _gameState = GameState.InGame;

        OnGameUnPaused.Invoke();
    }

    public void UnPauseGame(InputAction.CallbackContext ctx)
    {
        UnPauseGame();
    }

    private void Start()
    {
        _playerNavMeshAgent = _playerBehavior.GetComponent<NavMeshAgent>();
        _playerCollectible = _playerBehavior.GetComponent<PlayerCollectibleManager>();

        _initialPlayerSpeed = _playerNavMeshAgent.speed;

        _uiInputs.Enable();

        _uiInputs["Pause"].performed += PauseGame;
        _uiInputs["Start"].performed += StartGame;

        _uiInputs["UnPause"].performed += UnPauseGame;

        _uiInputs["Pause"].Disable();
        _uiInputs["UnPause"].Disable();

        _uiManager.OnInstructionFadeInEnd.AddListener(EnablePlayerControls);
        _playerCollectible.OnCoinAmmountIncrease.AddListener(CheckWinCondition);

        MainMenu();
    }

    // Update is called once per frame
    private void Update() { }

    private void MainMenu()
    {
        _uiManager.SetMainMenuUI();

        _playerBehavior.enabled = false;

        _gameState = GameState.MainMenu;
    }

    private void StartGame(InputAction.CallbackContext ctx)
    {
        Debug.Log("Start");

        _uiManager.StartGame();

        _uiInputs["Start"].Disable();

        OnGameStarted.Invoke();
    }

    private void CheckWinCondition(int? newCoinAmmount)
    {
        if (newCoinAmmount.Value >= _valueForTheWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        _uiManager.SetWinUI();

        _playerBehavior.enabled = false;
        _uiInputs["Pause"].Disable();

        _gameState = GameState.InWin;
    }

    private void EnablePlayerControls()
    {
        _uiInputs["Pause"].Enable();

        _playerBehavior.enabled = true;
        _playerNavMeshAgent.speed = _initialPlayerSpeed;

        _uiManager.SetGameUI();

        _playerBehavior.OnDestinationSelected.AddListener(OnFirstDestination);
    }

    private void OnFirstDestination()
    {
        _uiManager.FadeOutInstruction();
        _playerBehavior.OnDestinationSelected.RemoveListener(OnFirstDestination);
    }
}
