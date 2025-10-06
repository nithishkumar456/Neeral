using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;   // Resume / Settings / Quit
    [SerializeField] private GameObject settingsPanel;   // Audio / Video etc.

    private bool isPaused = false;
    private InputAction pauseAction;

    private void Awake()
    {
        // Get the System map and Pause action
        var playerInput = GetComponent<PlayerInput>();
        var systemMap = playerInput.actions.FindActionMap("System", true);
        pauseAction = systemMap.FindAction("Pause", true);

        // Ensure panels are hidden at start
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        pauseAction.performed += OnPausePressed;
        pauseAction.Enable(); // make sure it's active
    }

    private void OnDisable()
    {
        pauseAction.performed -= OnPausePressed;
        pauseAction.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext ctx)
    {
        if (isPaused) Resume();
        else Pause();
    }

    // === Called by input or Resume button ===
    public void Resume()
    {
        isPaused = false;
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // === Called by input ===
    public void Pause()
    {
        isPaused = true;
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    // === Called by Settings button ===
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // === Called by Back button in Settings ===
    public void BackToMain()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // === Called by Quit button ===
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
