using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadBook : MonoBehaviour
{
    public GameObject bookUI;

    private bool isOpen;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            bookUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void OpenBook()
    {
        if (!isOpen)
        {
            bookUI.SetActive(true);
            Time.timeScale = 0;
        }
        if (isOpen)
        {
            bookUI.SetActive(false);
        }
    }
}
