using System.Collections;
using UnityEngine;

public class VasePush : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public Transform vaseTrans;

    private bool isBroken = false;
    private bool canPush = true;

    private Rigidbody rb;
    [SerializeField] private Transform player;
    [SerializeField] private ParticleSystem particles;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // push the vase away from the player
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
            particles.Play();

            // disable the mesh renderer
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            mesh.enabled = false;

            // move the enemy to the vase
            vaseTrans = transform;
            enemyMovement.moveToVase(vaseTrans);

            Outline.Destroy(GetComponent<Outline>());
            Destroy(gameObject, 5);
        }
    }
}
