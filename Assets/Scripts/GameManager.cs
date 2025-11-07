using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    InGame,
    InPause,
};

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private AgentBehavior _playerBehavior;
    private NavMeshAgent _playerNavMeshAgent;

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
    }

    public void UnPauseGame(InputAction.CallbackContext ctx)
    {
        _uiInputs["UnPause"].Disable();

        _playerBehavior.enabled = true;
        _playerNavMeshAgent.speed = _initialPlayerSpeed;

        _uiManager.SetGameUI();

        _gameState = GameState.InGame;
    }

    private void Start()
    {
        _playerNavMeshAgent = _playerBehavior.GetComponent<NavMeshAgent>();
        _initialPlayerSpeed = _playerNavMeshAgent.speed;

        _uiInputs.Enable();

        _uiInputs["Pause"].performed += PauseGame;
        _uiInputs["Start"].performed += StartGame;

        _uiInputs["UnPause"].performed += UnPauseGame;

        _uiInputs["Pause"].Disable();
        _uiInputs["UnPause"].Disable();

        _uiManager.OnInstructionFadeInEnd.AddListener(EnablePlayerControls);

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
