using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPos;

    // move the camera every frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
