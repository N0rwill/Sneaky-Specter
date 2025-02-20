using UnityEngine;

interface IInteractable
{
    void Interact();
}

public class Interacter : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
