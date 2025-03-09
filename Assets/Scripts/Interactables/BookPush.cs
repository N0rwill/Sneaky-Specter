using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPush : MonoBehaviour
{
    private bool canPush = true;
    private bool isOnFloor = false;

    Rigidbody rb;
    public Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Push()
    {
        if (!isOnFloor && canPush)
        {
            canPush = false;
            Vector3 direction = new Vector3(transform.position.x - player.position.x, 0, transform.position.z - player.position.z).normalized;
            rb.AddForce(direction * 5, ForceMode.Impulse);
            StartCoroutine(PushCooldown());
        }
    }

    private IEnumerator PushCooldown()
    {
        yield return new WaitForSeconds(1f);
        if (!isOnFloor)
        {
            canPush = true;
        }
    }

    // when colliding with the ground play the sound and disable the collider
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isOnFloor = true;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            Outline.Destroy(GetComponent<Outline>());
        }
    }
}
