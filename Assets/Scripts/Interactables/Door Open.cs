using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject door;
    public bool isOpen = false;

    public int lampCount = 0;

    public void OpenDoor(int lampCount)
    {
        if (!isOpen)
        {
            door.transform.Rotate(0, 90, 0);
            isOpen = true;
        }
    }

    public void LampCounter()
    {
        lampCount++;

        if (lampCount == 3)
        {
            OpenDoor(lampCount);
        }
    }
}
