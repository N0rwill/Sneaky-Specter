using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public HudController hudController;

    private Transform door;
    private bool isOpen = false;
    public bool isUnlocked = false;
    [SerializeField] private AudioSource lockedDoorAudio;
    [SerializeField] private AudioSource OpeningDoorAudio;
    [SerializeField] private AudioSource allSwitchesAudio;

    [SerializeField] private GameObject doorLight1;
    [SerializeField] private GameObject doorLight2;
    [SerializeField] private GameObject doorLight3;

    public int switchCount = 0;

    private void Start()
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

    // Open the door
    private void OpenDoor()
    {
        if (isUnlocked && !isOpen)
        {
            StartCoroutine(SmoothRotate(door, Vector3.up * -90, 5f));
            isOpen = true;
            OpeningDoorAudio.Play();
        }
    }

    // counts how many switches have been activated
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
        // unlock the door if all switches have been activated
        if (switchCount >= 3)
        {
            doorLight3.SetActive(false);
            allSwitchesAudio.Play();
            isUnlocked = true;
        }
    }

    // Smoothly rotate the door
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
