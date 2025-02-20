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
        interactText.text = text + " (E)";
        interactText.gameObject.SetActive(true);
    }

    public void DisableInteractText()
    {
        interactText.gameObject.SetActive(false);
    }
}
