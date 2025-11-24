using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject pauseMenuPanel;

    public GameObject winner1; 
    public GameObject winner2;

    private bool isGameRunning = false;
    private bool isGamePaused = false;
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Gameplay.Pause.performed += OnPausePressed;
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Pause.performed -= OnPausePressed;
        input.Gameplay.Disable();
    }

    public void Start()
    {
        startMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (!isGameRunning) { return; }

        if (isGamePaused) ResumeGame();
        else PauseGame();
    }

    public void StartGame()
    {
        startMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isGameRunning = true;

        winner1.SetActive(false);
        winner2.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
