using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public PlayerCam playerCam;

    public Transform camPos;
    public Transform orientation;

    void Update()
    {
        // Move the hand down and forwards relative to the camera's position
        transform.position = camPos.position;
        // Set the hand's rotation to match the orientation
        transform.rotation = Quaternion.Euler(orientation.rotation.eulerAngles.x + playerCam.xRotation, orientation.rotation.eulerAngles.y, orientation.rotation.eulerAngles.z);
    }
}
