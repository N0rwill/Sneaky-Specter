using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FlipSwitch : MonoBehaviour
{
    public DoorOpen doorOpen;
    public GameObject hinge;

    bool isFlipped = false;

    void Start()
    {
        hinge.transform.Rotate(0, 0, 50);
    }

    public void Flip()
    {
        if (!isFlipped)
        {
            isFlipped = true;
            doorOpen.SwitchCounter();
            StartCoroutine(SmoothRotate(hinge.transform, new Vector3(0, 0, -110), 0.5f));

            AudioSource audio = GetComponent<AudioSource>();
            // audio.Play();
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
