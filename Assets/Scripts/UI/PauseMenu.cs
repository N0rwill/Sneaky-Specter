using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public ReadBook readBook;

    public GameObject pauseMenu;
    public GameObject playingUI;
    public GameObject settingsMenu;

    public bool isPaused = false;

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
