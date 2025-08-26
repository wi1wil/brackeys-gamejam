using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    public Vector2 movementInput;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 movementDir = movementInput.normalized;
        rb.velocity = movementDir * speed;
    }
}
