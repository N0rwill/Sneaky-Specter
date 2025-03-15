using System.Collections;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
    public DoorOpen doorOpen;
    public GameObject hinge;

    private bool isFlipped = false;

    private void Start()
    {
        hinge.transform.Rotate(0, 0, 50);
    }

    // Flip the switch
    public void Flip()
    {
        if (!isFlipped)
        {
            isFlipped = true;
            doorOpen.SwitchCounter();
            StartCoroutine(SmoothRotate(hinge.transform, new Vector3(0, 0, -110), 0.5f));

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    // smoothly Rotate the switch
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
