using UnityEngine;

public class HelloWorld : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Hello World!");
    }
}
