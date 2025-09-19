using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;      // move Speed
    public float jump = 9f;       // jump power
    public int jumpsMax = 2;      // how many jumps
    public GameObject startPanel; 

    Rigidbody2D rb;
    float moveX = 0f;
    int jumps = 0;
    bool started = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (startPanel) startPanel.SetActive(true);
        Time.timeScale = 0f; // game paused until start
    }

    void FixedUpdate()
    {
        if (!started) return;
        var v = rb.linearVelocity; 
        v.x = moveX * speed;
        rb.linearVelocity = v;
    }

    // Input System: Gameplay / Move
    public void OnMove(InputAction.CallbackContext c)
    {
        moveX = c.ReadValue<Vector2>().x;
        if (c.canceled) moveX = 0f; // stop when key released
    }

    // Input Syste Gameplay / Jump (space)
    public void OnJump(InputAction.CallbackContext c)
    {
        if (!started) return;
        if (!c.performed) return;

        if (jumps < jumpsMax)
        {
            var v = rb.linearVelocity;
            v.y = jump;
            rb.linearVelocity = v;
            jumps++;
        }
    }

    //Input System: Gameplay /Start (Enter)
    public void OnStart(InputAction.CallbackContext c)
    {
        if (c.performed && !started)
        {
            started = true;
            if (startPanel) startPanel.SetActive(false);
            Time.timeScale = 1f; // unpause
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground")) jumps = 0; // reset jumps
    }
}
