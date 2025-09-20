using UnityEngine;

public class Bullet : MonoBehaviour

{
    public float speed = 10f, lifetime = 3f;


    private void Start() { 

    Destroy(gameObject, lifetime); 

    }


    private void Update() { 

    transform.Translate(Vector2.right * speed * Time.deltaTime); 
    
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Door")) Destroy(gameObject);
    }
}

