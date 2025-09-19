using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;        // change in thje inspector

    Rigidbody2D rb;
    float x;                        // -1..1 from input

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // horizontal move (new API in Unity 6)
        Vector2 v = rb.linearVelocity;
        v.x = x * speed;
        rb.linearVelocity = v;
    }

    // hooked to PlayerInput -> Gameplay/Move (Invoke Unity Events)
    public void OnMove(InputAction.CallbackContext ctx)
    {
        x = ctx.ReadValue<Vector2>().x;
        if (ctx.canceled) x = 0f;   // stop when key released
    }
}
