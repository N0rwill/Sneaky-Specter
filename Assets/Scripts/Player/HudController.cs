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
        // enable the text
        interactText.gameObject.SetActive(true);
    }

    public void DisableInteractText()
    {
        // disable the text
        interactText.gameObject.SetActive(false);
    }

    public void SeceritySystemStillOn()
    {
        interactText.text = "Can Not Open Security System Activated";

        interactText.gameObject.SetActive(true);
        tryingToUnlock = true;
        StartCoroutine(DisableTextAfterDelay(3f));
    }

    private IEnumerator DisableTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tryingToUnlock = false;
        interactText.gameObject.SetActive(false);
    }
}
