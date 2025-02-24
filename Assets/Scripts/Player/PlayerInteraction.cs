using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    public Transform cam;
    Interactable currentInteractable;

    void Update()
    {
        CheckInteractable();
        if (Input.GetButtonDown("Interact") && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteractable()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.position, cam.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

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

    void OnDrawGizmos()
    {
        if (cam != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cam.position, cam.forward * playerReach);
        }
    }
}
