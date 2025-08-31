using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    public Vector2 movementInput;

    private Rigidbody2D rb;
    SlotMachineScript slotMachineScript;
    DiarrheaPillsScript diarrheaPillsScript;
    InteractableDetectorScript interactableDetectorScript;
    VolumeSettingScript volumeSettingScript;
    FemaleRatScript femaleRatScript;

    void Start()
    {
        volumeSettingScript = FindAnyObjectByType<VolumeSettingScript>();
        femaleRatScript = FindAnyObjectByType<FemaleRatScript>();
        diarrheaPillsScript = FindAnyObjectByType<DiarrheaPillsScript>();
        slotMachineScript = FindAnyObjectByType<SlotMachineScript>();
        interactableDetectorScript = FindAnyObjectByType<InteractableDetectorScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (slotMachineScript.isGambling || diarrheaPillsScript.confirmationPanelOpened || femaleRatScript.isConversationActivated || interactableDetectorScript.isHolding || volumeSettingScript.menuPanel.activeSelf)
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
