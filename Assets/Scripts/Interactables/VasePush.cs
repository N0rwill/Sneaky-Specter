using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VasePush : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    Transform vaseTrans;

    private bool isBroken = false;
    private bool canPush = true;

    Rigidbody rb;
    public Transform player;
    [SerializeField] private ParticleSystem particles;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Push()
    {
        if (!isBroken && canPush)
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
        if (!isBroken)
        {
            canPush = true;
        }
    }

    // when colliding with the ground the vase will break
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && !isBroken)
        {
            isBroken = true;
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = false;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            mesh.enabled = false;
            particles.Play();
            Outline.Destroy(GetComponent<Outline>());

            vaseTrans = transform;
            enemyMovement.moveToVase(vaseTrans);

            Destroy(gameObject, 5);
        }
    }
}
