using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAnimation : MonoBehaviour
{
    public Transform bookTransform;
    public float levitationHeight = 0.5f;
    public float levitationSpeed = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        bookTransform = GetComponent<Transform>();
        initialPosition = bookTransform.position;
    }

    void Update()
    {
        Levitate();
    }

    private void Levitate()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        bookTransform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
