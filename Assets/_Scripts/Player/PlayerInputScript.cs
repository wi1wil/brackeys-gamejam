using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    public Vector2 movementInput;

    private Rigidbody2D rb;

    PlayerLadderScript playerLadderScript;

    void Start()
    {
        playerLadderScript = GetComponent<PlayerLadderScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerLadderScript.isClimbing) return;
        movementInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 movementDir = movementInput.normalized;
        rb.velocity = movementDir * speed;
    }
}
