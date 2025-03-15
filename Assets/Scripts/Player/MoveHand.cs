using UnityEngine;

public class MoveHand : MonoBehaviour
{
    public PlayerCam playerCam;

    [SerializeField] private Transform camPos;
    [SerializeField] private Transform orientation;

    private void Update()
    {
        // Move the hand down and forwards relative to the camera's position
        transform.position = camPos.position;
        // Set the hand's rotation to match the orientation
        transform.rotation = Quaternion.Euler(orientation.rotation.eulerAngles.x + playerCam.xRotation, orientation.rotation.eulerAngles.y, orientation.rotation.eulerAngles.z);
    }
}
