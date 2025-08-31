using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    private Transform playerTransform;

    StagesScript stagesScript;
    PlayerInputScript playerInputScript;
    ObesityScript obesityScript;
    FemaleRatScript femaleRatScript;
    GameOverScript gameOverScript;

    void Start()
    {
        femaleRatScript = FindAnyObjectByType<FemaleRatScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        gameOverScript = FindAnyObjectByType<GameOverScript>();
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
            femaleRatScript.hasAlreadyBought = false;
            obesityScript.ResetVariables();
            stagesScript.currentStageLevel++;
        }
        else if (stagesScript.currentStageLevel == 3)
        {
            stagesScript.currentStageLevel++;
            Debug.Log("Player has won!");
            gameOverScript.playerWon();
        }
    }
}
 