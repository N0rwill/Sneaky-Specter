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

            hinge.transform.Rotate(0, 0, -70);

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            doorOpen.SwitchCounter();
        }
    }
}
