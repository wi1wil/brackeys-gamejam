using UnityEngine;
using Cinemachine;

public class CabinetScript : MonoBehaviour, IInteractable
{
    private Transform playerTransform;
    private Vector3 originalPlayerPos;
    [SerializeField] private Transform hidingPos;

    private Collider2D originalBounds;
    [SerializeField] private Collider2D hidingBounds;

    PlayerInputScript playerInputScript;
    CinemachineConfiner2D cinemachineConfiner2D;

    void Start()
    {
        cinemachineConfiner2D = FindObjectOfType<CinemachineConfiner2D>();
        playerInputScript = FindObjectOfType<PlayerInputScript>();
        playerTransform = playerInputScript.transform;
        
    }

    public void Interact()
    {
        if (!HidingManager.isHiding)
        {
            HidingManager.originalPlayerPos = playerTransform.position;
            HidingManager.originalCameraBounds = cinemachineConfiner2D.m_BoundingShape2D;

            playerTransform.position = hidingPos.position;
            cinemachineConfiner2D.m_BoundingShape2D = hidingBounds;
            HidingManager.isHiding = true;
        }
        else
        {
            playerTransform.position = HidingManager.originalPlayerPos;
            cinemachineConfiner2D.m_BoundingShape2D = HidingManager.originalCameraBounds;
            HidingManager.isHiding = false;
        }
    }
}
