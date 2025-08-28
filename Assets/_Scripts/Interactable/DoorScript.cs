using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    private Transform playerTransform;

    StagesScript stagesScript;
    PlayerInputScript playerInputScript;
    ObesityScript obesityScript;

    void Start()
    {
        obesityScript = FindAnyObjectByType<ObesityScript>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        playerTransform = playerInputScript.transform;
        stagesScript = FindAnyObjectByType<StagesScript>();
    }

    public void Interact()
    {
        // Teleport to next stage;
        Debug.Log("Im gonna be teleported into the next stage!");
        playerTransform.position = stagesScript.playerSpawnLocations[stagesScript.currentStageLevel].position;
        if (stagesScript.currentStageLevel < 3)
        {
            Debug.Log($"Going to stage {stagesScript.currentStageLevel}");
            obesityScript.ResetVariables();
            stagesScript.currentStageLevel++;
        }
    }
}
 