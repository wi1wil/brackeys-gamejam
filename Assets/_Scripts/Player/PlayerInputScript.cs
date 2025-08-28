using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    public Vector2 movementInput;

    private Rigidbody2D rb;
    SlotMachineScript slotMachineScript;


    void Start()
    {
        slotMachineScript = FindAnyObjectByType<SlotMachineScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (slotMachineScript.isGambling)
        {
            movementInput = Vector2.zero;
            return;
        }

        movementInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (slotMachineScript.isGambling)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 movementDir = movementInput.normalized;
        rb.velocity = movementDir * speed;
    }
}
