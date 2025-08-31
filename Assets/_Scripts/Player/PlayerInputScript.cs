using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    public Vector2 movementInput;
    private Animator animator;
    private Rigidbody2D rb;

    SlotMachineScript slotMachineScript;
    DiarrheaPillsScript diarrheaPillsScript;
    InteractableDetectorScript interactableDetectorScript;
    PlayerHealthScript playerHealthScript;
    VolumeSettingScript volumeSettingScript;
    FemaleRatScript femaleRatScript;
    GameOverScript gameOverScript;

    void Start()
    {
        gameOverScript = FindAnyObjectByType<GameOverScript>();
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        volumeSettingScript = FindAnyObjectByType<VolumeSettingScript>();
        femaleRatScript = FindAnyObjectByType<FemaleRatScript>();
        diarrheaPillsScript = FindAnyObjectByType<DiarrheaPillsScript>();
        slotMachineScript = FindAnyObjectByType<SlotMachineScript>();
        interactableDetectorScript = FindAnyObjectByType<InteractableDetectorScript>();
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!gameOverScript.isAlive)
        {
            movementInput = Vector2.zero;
            return;
        }

        if (
            slotMachineScript.isGambling ||
            diarrheaPillsScript.confirmationPanelOpened ||
            femaleRatScript.isConversationActivated ||
            interactableDetectorScript.isHolding ||
            volumeSettingScript.menuPanel.activeSelf ||
            gameOverScript.gameOverPanel.activeSelf ||
            gameOverScript.startMenuPanel.activeSelf
        )
        {
            movementInput = Vector2.zero;
            return;
        }

        if (context.canceled) animator.SetBool("isWalking", false);

        animator.SetBool("isWalking", true);
        movementInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movementInput.x);
        animator.SetFloat("InputY", movementInput.y);
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