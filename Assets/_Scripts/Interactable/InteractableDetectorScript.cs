using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableDetectorScript : MonoBehaviour
{
    private IInteractable InteractableInRange = null;
    private Biscuit BiscuitInRange = null;
    public GameObject interactionIcon;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            if(InteractableInRange != null && !(InteractableInRange is Biscuit))
                InteractableInRange?.Interact();
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.performed && BiscuitInRange != null)
            BiscuitInRange?.Collect();
    }

    public void OnEat(InputAction.CallbackContext context)
    {
        if (context.performed && BiscuitInRange != null)
            BiscuitInRange?.Eat();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Biscuit biscuit))
        {
            BiscuitInRange = biscuit;
            interactionIcon.SetActive(true);
        }
        else if (collision.TryGetComponent(out IInteractable interactable))
        {
            InteractableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Biscuit biscuit) && biscuit == BiscuitInRange)
        {
            BiscuitInRange = null;
            InteractableInRange = null;
            interactionIcon.SetActive(false);
        }
        else if (collision.TryGetComponent(out IInteractable interactable) && interactable == InteractableInRange)
        {
            InteractableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
