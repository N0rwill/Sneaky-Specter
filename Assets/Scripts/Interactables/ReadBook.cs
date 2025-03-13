using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadBook : MonoBehaviour
{
    public GameObject bookUI;
    public GameObject PlayingUI;

    public bool isOpen;

    public void InteractWithBook()
    {
        if (!isOpen)
        {
            OpenBook();
        }
        else if (isOpen)
        {
            CloseBook();
        }
    }

    public void OpenBook()
    {
        bookUI.SetActive(true);
        Time.timeScale = 0;
        PlayingUI.SetActive(false);
        isOpen = true;
    }

    public void CloseBook()
    {
        bookUI.SetActive(false);
        Time.timeScale = 1;
        PlayingUI.SetActive(true);
        isOpen = false;
    }
}
