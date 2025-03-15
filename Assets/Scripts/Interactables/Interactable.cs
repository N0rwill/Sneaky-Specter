using UnityEngine;
using UnityEngine.Events;

// controller for interactable objects
public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;

    public UnityEvent onInteract;

    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteract.Invoke();
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
