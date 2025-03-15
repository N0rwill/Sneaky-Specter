using System.Collections;
using UnityEngine;

public class BookPush : MonoBehaviour
{
    private bool canPush = true;
    private bool isOnFloor = false;

    private Rigidbody rb;
    [SerializeField] private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Push the book if it is not on the floor
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

    // Cooldown for pushing the book
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
