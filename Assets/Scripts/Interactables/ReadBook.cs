using UnityEngine;

public class ReadBook : MonoBehaviour
{
    [SerializeField] private GameObject bookUI;
    [SerializeField] private GameObject PlayingUI;

    public bool isOpen;

    // called by the interactable object
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

    private void OpenBook()
    {
        bookUI.SetActive(true);
        Time.timeScale = 0;
        PlayingUI.SetActive(false);
        isOpen = true;
    }

    private void CloseBook()
    {
        bookUI.SetActive(false);
        Time.timeScale = 1;
        PlayingUI.SetActive(true);
        isOpen = false;
    }
}
