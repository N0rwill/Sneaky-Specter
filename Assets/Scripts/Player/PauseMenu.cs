using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool isPause;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButton("Menu"))
        {
            PauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
