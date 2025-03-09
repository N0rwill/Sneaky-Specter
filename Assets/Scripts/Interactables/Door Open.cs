using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpen : MonoBehaviour
{
    public HudController hudController;

    public Transform door;
    public bool isOpen = false;
    public bool isUnlocked = false;

    public int switchCount = 0;

    void Start()
    {
        door = GetComponent<Transform>();
    }

    public void Interact()
    {
        if (isUnlocked)
        {
            OpenDoor();
        }
        else
        {
            hudController.SeceritySystemStillOn();
        }
    }

    public void OpenDoor()
    {
        if (isUnlocked && !isOpen)
        {
            StartCoroutine(SmoothRotate(door, Vector3.up * -90, 1f));
            isOpen = true;
        }
    }

    public void SwitchCounter()
    {
        switchCount++;

        if (switchCount == 3)
        {
            isUnlocked = true;
        }
    }

    private IEnumerator SmoothRotate(Transform target, Vector3 byAngles, float duration)
    {
        Quaternion fromRotation = target.rotation;
        Quaternion toRotation = target.rotation * Quaternion.Euler(byAngles);
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            target.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.rotation = toRotation;
    }
}
