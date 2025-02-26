using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VasePush : MonoBehaviour
{
    private bool isBroken = false;
    private bool isPushed = false;

    Rigidbody rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Push()
    {
        if (!isPushed)
        {
            isPushed = true;
            Vector3 direction = new Vector3(transform.position.x - player.position.x, 0, transform.position.z - player.position.z).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);
        }
    }

    // when colliding witht the ground the vase will break
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !isBroken)
        {
            isBroken = true;
            //AudioSource audio = GetComponent<AudioSource>();
            //audio.Play();

            Destroy(gameObject, 1);
        }
    }
}
