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
    [SerializeField] private AudioSource lockedDoorAudio;
    [SerializeField] private AudioSource OpeningDoorAudio;
    public AudioSource allSwitchesAudio;

    public GameObject doorLight1;
    public GameObject doorLight2;
    public GameObject doorLight3;

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
            lockedDoorAudio.Play();
        }
    }

    public void OpenDoor()
    {
        if (isUnlocked && !isOpen)
        {
            StartCoroutine(SmoothRotate(door, Vector3.up * -90, 5f));
            isOpen = true;
            OpeningDoorAudio.Play();
        }
    }

    public void SwitchCounter()
    {
        switchCount++;

        if (switchCount >= 1)
        {
            doorLight1.SetActive(false);
        }
        if (switchCount >= 2)
        {
            doorLight2.SetActive(false);
        }
        if (switchCount >= 3)
        {
            doorLight3.SetActive(false);
            allSwitchesAudio.Play();
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
