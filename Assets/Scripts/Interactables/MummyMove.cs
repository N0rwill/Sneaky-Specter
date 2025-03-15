using UnityEngine;

public class MummyMove : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // play the animation
    public void SitUp()
    {
        anim.enabled = true;
    }
}