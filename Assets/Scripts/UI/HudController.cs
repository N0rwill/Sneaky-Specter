using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    public bool tryingToUnlock = false;

    void Awake()
    {
        instance = this;
        tryingToUnlock = false;
    }

    [SerializeField] TMP_Text interactText;

    public void EnanbleInteractText(string text)
    {
        // set the text to whatevers in the inspector + (E)
        interactText.text = text + " (E)";
        interactText.gameObject.SetActive(true);
    }

    public void DisableInteractText()
    {
        interactText.gameObject.SetActive(false);
    }
}
