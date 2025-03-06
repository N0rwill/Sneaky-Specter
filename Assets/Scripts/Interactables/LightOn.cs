using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightOn : MonoBehaviour
{
    public DoorOpen doorOpen;

    bool isOn = false;
    public UnityEvent turnOn;

    public void FlipSwitch()
    {
        if (!isOn)
        {
            isOn = true;
            turnOn.Invoke();

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            doorOpen.SwitchCounter();
        }
    }
}
