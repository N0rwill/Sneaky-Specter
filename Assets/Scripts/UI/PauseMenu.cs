using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public ReadBook readBook;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playingUI;
    [SerializeField] private GameObject settingsMenu;

    private bool isPaused = false;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !readBook.isOpen)
        {
            if (isPaused)
            {
                ResumeGame();
                SettingsClose();
            }
            else
            {
                PauseGame();
            }
        }

        // Cursor lock
        if (isPaused && pauseMenu != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        playingUI.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        playingUI.SetActive(true);
    }

    public void SettingsClose()
    {
        settingsMenu.SetActive(false);
    }
}
