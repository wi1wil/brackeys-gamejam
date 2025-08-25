using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetectorScript : MonoBehaviour
{
    private IInteractable InteractableInRange = null;
    public GameObject interactionIcon;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractableInRange?.Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            InteractableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            InteractableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
