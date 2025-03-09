using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPush : MonoBehaviour
{
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

    // when colliding with the ground play the sound and disable the collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            Outline.Destroy(GetComponent<Outline>());
        }
    }
}
