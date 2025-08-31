using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractableDetectorScript : MonoBehaviour
{
    private IInteractable InteractableInRange = null;
    private Biscuit BiscuitInRange = null;
    public GameObject interactionIcon;

    public Image fillCircle;
    public float holdDuration = 1;
    public float holdTimer;

    public bool isHolding = false;
    public bool isEating = false;
    public bool isCollecting = false;

    PlayerInputScript playerInputScript;
    AudioManagerScript audioManagerScript;

    void Start()
    {
        audioManagerScript = FindAnyObjectByType<AudioManagerScript>();
        playerInputScript = FindObjectOfType<PlayerInputScript>();
    }

    void Update()
    {
        if (isHolding)
        {
            fillCircle.gameObject.SetActive(true);
            Vector3 worldPos = playerInputScript.transform.position + Vector3.up * 1.5f;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            fillCircle.transform.position = screenPos;

            holdTimer += Time.deltaTime;
            fillCircle.fillAmount = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                if (isCollecting)
                {
                    BiscuitInRange.Collect();
                    audioManagerScript.PlaySFX(audioManagerScript.CollectSFX);
                }
                else if (isEating)
                {
                    BiscuitInRange.Eat();
                    audioManagerScript.PlaySFX(audioManagerScript.EatSFX);
                }
                ResetHold();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            if (InteractableInRange != null && !(InteractableInRange is Biscuit))
                InteractableInRange?.Interact();
    }

    public void OnCollect(InputAction.CallbackContext context)
    {
        if (context.started && BiscuitInRange != null)
        {
            isHolding = true;
            isCollecting = true;
        }
        else if (context.canceled || BiscuitInRange == null)
        {
            ResetHold();
        }   
    }

    public void OnEat(InputAction.CallbackContext context)
    {
        if (context.started && BiscuitInRange != null)
        {
            isHolding = true;
            isEating = true;
        }
        else if (context.canceled || BiscuitInRange == null)
        {
            ResetHold();
        }   
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
            interactionIcon.SetActive(InteractableInRange != null);
        }
        else if (collision.TryGetComponent(out IInteractable interactable) && interactable == InteractableInRange)
        {
            InteractableInRange = null;
            interactionIcon.SetActive(false);
        }
    }

    public void ResetHold()
    {
        isHolding = false;
        isEating = false;
        isCollecting = false;
        fillCircle.gameObject.SetActive(false);
        holdTimer = 0f;
    }
}
