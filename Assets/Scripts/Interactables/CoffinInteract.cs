using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffinInteract : MonoBehaviour
{
    public GameManager gameManager;
    public MummyMove mummyMove;

    private bool canPush = true;
    private bool isMoving = false;
    private bool isGrounded = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Push()
    {
        if (canPush && !isMoving)
        {
            canPush = false;
            isMoving = true;
            StartCoroutine(MoveCoffin());
        }
    }

    private IEnumerator MoveCoffin()
    {
        rb.isKinematic = false;
        float moveSpeed = 10f;
        float rotationSpeed = 0.25f;

        while (!isGrounded)
        {
            rb.MovePosition(transform.position + Vector3.forward * moveSpeed * Time.deltaTime);
            rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, rotationSpeed));
            yield return null;
        }

        rb.isKinematic = true;
        isMoving = false;

        yield return new WaitForSeconds(2f);
        mummyMove.SitUp();

        yield return new WaitForSeconds(4f);
        gameManager.Win();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
}
