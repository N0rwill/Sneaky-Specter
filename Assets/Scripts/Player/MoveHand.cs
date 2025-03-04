using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public Transform camPos;
    public Transform orientation;

    void Update()
    {
        // Move the hand down and forwards relative to the camera's position
        transform.position = camPos.position + orientation.forward * 1f - camPos.up * 3.5f;
        // Set the hand's rotation to match the orientation
        transform.rotation = Quaternion.Euler(orientation.rotation.eulerAngles.x - 90, orientation.rotation.eulerAngles.y, orientation.rotation.eulerAngles.z);
    }
}
