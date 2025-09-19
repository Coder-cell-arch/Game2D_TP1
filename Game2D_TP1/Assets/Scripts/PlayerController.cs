using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 9f;
    public int maxJumps = 2;

    Rigidbody2D rb;
    float x;          // -1..1 
    int jumpsUsed = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // left / right
        rb.linearVelocity = new Vector2(x * speed, rb.linearVelocity.y);
    }

    // Input System → Gameplay/Move
    public void OnMove(InputAction.CallbackContext ctx)
    {
        x = ctx.ReadValue<Vector2>().x;
    }

    // Input System → Gameplay/Jump
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        if (jumpsUsed < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsUsed++;
        }
    }

    // reset When touched ground
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            jumpsUsed = 0;
    }
}
