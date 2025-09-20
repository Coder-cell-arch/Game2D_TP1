using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 2f;

    void OnEnable()
    {
        Destroy(gameObject, life);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.isTrigger) Destroy(gameObject);
    }
}
