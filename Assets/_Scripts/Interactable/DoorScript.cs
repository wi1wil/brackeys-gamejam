using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    private Transform playerTransform;

    StagesScript stagesScript;
    PlayerInputScript playerInputScript;

    void Start()
    {
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        playerTransform = playerInputScript.transform;
        stagesScript = FindAnyObjectByType<StagesScript>();
    }

    public void Interact()
    {
        // Teleport to next stage;
        Debug.Log("Im gonna be teleported into the next stage!");
        playerTransform.position = stagesScript.playerSpawnLocations[stagesScript.currentStageLevel].position;
    }
}
 