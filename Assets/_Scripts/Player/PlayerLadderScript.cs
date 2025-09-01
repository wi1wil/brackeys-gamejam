using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLadderScript : MonoBehaviour
{
    private Vector2 movementInput;
    private float speed = 6f;
    public bool isClimbing;
    private bool isLadder;

    Rigidbody2D rb;

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
        float vertical = movementInput.y;

        if (isLadder && Mathf.Abs(vertical) > 0.1f)
        {
            isClimbing = true;
        }

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * speed);
        }
    }
     
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            Debug.Log("Am on a ladder!");
            isLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
