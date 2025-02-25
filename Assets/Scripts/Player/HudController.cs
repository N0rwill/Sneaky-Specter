using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactText;

    public void EnanbleInteractText(string text)
    {
        // set the text to whatevers in the inspector + (E)
        interactText.text = text + " (E)";
        // enable the text
        interactText.gameObject.SetActive(true);
    }

    public void DisableInteractText()
    {
        // disable the text
        interactText.gameObject.SetActive(false);
    }
}
