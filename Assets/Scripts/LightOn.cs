using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightOn : MonoBehaviour
{
    bool isOn = false;
    public UnityEvent turnOn, turnOff;

    public void FlipSwitch()
    {
        if (isOn)
        {
            isOn = false;
            turnOff.Invoke();
        }
        else
        {
            isOn = true;
            turnOn.Invoke();
        }
    }
}
