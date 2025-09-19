using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // move & jump
    public float speed = 5f;
    public float jump = 9f;
    public int jumpsMax = 2;

    // start panel
    public GameObject startPanel;

    // shooting
    public GameObject bullet;
    public float bulletSpeed = 12f;

    Rigidbody2D rb;
    float mx = 0f;
    int j = 0;
    bool started = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (startPanel) startPanel.SetActive(true);
        Time.timeScale = 0f; // paused until start
    }

    void FixedUpdate()
    {
        if (!started) return;

        Vector2 v = rb.linearVelocity;
        v.x = mx * speed;
        rb.linearVelocity = v;
    }

    // Input: Gameplay/ Move
    public void OnMove(InputAction.CallbackContext c)
    {
        mx = c.ReadValue<Vector2>().x;
        if (c.canceled) mx = 0f;
    }

    // Input:Gamepla / Jump (space)
    public void OnJump(InputAction.CallbackContext c)
    {
        if (!started) return;
        if (!c.performed) return;

        if (j < jumpsMax)
        {
            Vector2 v = rb.linearVelocity;
            v.y = jump;
            rb.linearVelocity = v;
            j++;
        }
    }

    // Input: Gameplay /Start (Enter)
    public void OnStart(InputAction.CallbackContext c)
    {
        if (c.performed && !started)
        {
            started = true;
            if (startPanel) startPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            j = 0; // reset jumps
        }
    }

    // Input: Gameplay / Shoot(C)
    public void OnShoot(InputAction.CallbackContext c)
    {
        if (!started) return;
        if (!c.performed) return;
        if (bullet == null) return;

        float d = (mx >= 0f) ? 1f : -1f;

        Vector3 p = transform.position + new Vector3(0.6f * d, 0f, 0f);
        GameObject b = Instantiate(bullet, p, Quaternion.identity);

        Rigidbody2D r = b.GetComponent<Rigidbody2D>();
        if (r) r.linearVelocity = new Vector2(bulletSpeed * d, 0f);
    }
}
