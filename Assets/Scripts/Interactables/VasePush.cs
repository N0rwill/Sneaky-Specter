using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VasePush : MonoBehaviour
{
    private bool isBroken = false;
    Rigidbody rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Push()
    {
        if (!isBroken)
        {
            isBroken = true;
            Vector3 direction = new Vector3(transform.position.x - player.position.x, 0, transform.position.z - player.position.z).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);
        }
    }
}
