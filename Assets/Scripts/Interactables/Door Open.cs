using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform door;
    public bool isOpen = false;

    public int switchCount = 0;

    void Start()
    {
        door = GetComponent<Transform>();
    }

    public void OpenDoor(int switchCount)
    {
        if (!isOpen)
        {
            door.Rotate(0, -90, 0);
            isOpen = true;
        }
    }

    public void SwitchCounter()
    {
        switchCount++;

        if (switchCount == 3)
        {
            OpenDoor(switchCount);
        }
    }
}
