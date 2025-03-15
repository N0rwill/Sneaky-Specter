using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public HudController hudController;

    public Transform cam;
    public Interactable currentInteractable;
    [SerializeField] private float playerReach = 7.5f;

    [SerializeField] private Animator amin;

    void Update()
    {
        CheckInteractable();
        if (Input.GetButtonDown("Interact") && currentInteractable != null)
        {
            currentInteractable.Interact();
            amin.SetTrigger("Push");
        }
    }

    void CheckInteractable()
    {
        // Raycast to check for interactables
        RaycastHit hit;
        Ray ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                // get the interactable component
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                // if the new interactable is different from the current one
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    DisableCurrentInteractable();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HudController.instance.EnanbleInteractText(currentInteractable.message);
    }

    void DisableCurrentInteractable()
    {
        HudController.instance.DisableInteractText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(cam.position, cam.forward * playerReach);
    }
}
