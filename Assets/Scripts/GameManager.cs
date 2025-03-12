using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("EarlyLevel");
        Debug.Log("Game has loaded.");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
        Debug.Log("Returned to Menu.");
    }

    public void Lose()
    {
        SceneManager.LoadScene("Lose");
        Debug.Log("Lose has loaded.");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
        Debug.Log("Win has loaded.");
    }

    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
