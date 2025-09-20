using UnityEngine;

public class DummyHit : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        if (!anim) anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (anim) anim.SetTrigger("Hit");
            Destroy(other.gameObject);
        }
    }
}
