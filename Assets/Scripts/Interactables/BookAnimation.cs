using UnityEngine;

public class BookAnimation : MonoBehaviour
{
    private Transform bookTransform;
    private float levitationHeight = 0.5f;
    private float levitationSpeed = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        bookTransform = GetComponent<Transform>();
        initialPosition = bookTransform.position;
    }

    private void Update()
    {
        Levitate();
    }

    // makes book move up and down
    private void Levitate()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        bookTransform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
