using UnityEngine;

public class ObesityScript : MonoBehaviour
{
    public int obesityType = 0;
    public int overEatenBiscuits = 0;
    public int totalBiscuits = 0;
    public int minimumPass = 5;

    public bool isDiarrhea = false;
    public bool hasMinimum = false;
    public bool minimumReached = false;
    public GameObject doorPrefab;
    public GameObject spawnParent;

    PlayerInputScript playerInputScript;
    GameOverScript gameOverScript;
    StagesScript stagesScript;

    void Start()
    {
        stagesScript = FindObjectOfType<StagesScript>();
        gameOverScript = FindObjectOfType<GameOverScript>();
        playerInputScript = FindObjectOfType<PlayerInputScript>();
    }

    public void ResetVariables()
    {
        hasMinimum = false;
        minimumReached = false;
        totalBiscuits = 0;
        overEatenBiscuits = 0;
    }

    public void AddType()
    {
        totalBiscuits++;
        Debug.Log($"Eated: {overEatenBiscuits}, Total: {totalBiscuits}");
        if (totalBiscuits >= minimumPass)
        {
            Debug.Log("Minimum Reached");
            hasMinimum = true;
        }

        if (hasMinimum)
        {
            overEatenBiscuits++;
        }
        if (overEatenBiscuits >= 2 && hasMinimum && !isDiarrhea)
        {
            obesityType++;
            CheckObesityType();
            overEatenBiscuits = 0;
        }

        if (hasMinimum && !minimumReached)
        {
            minimumReached = true;
            Debug.Log("Minimum reached, door spawned");
            int current = stagesScript.currentStageLevel;
            int total = stagesScript.stages[current - 1].totalSpawnLocation;
            int random = Random.Range(1, total + 1);
            Vector2 spawnPos = stagesScript.stages[current - 1].spawnPositions[random - 1].transform.position;
            Instantiate(doorPrefab, spawnPos, Quaternion.Euler(0f, 0f, 90f), spawnParent.transform);

            // Spawn all
            // int current = stagesScript.currentStageLevel;
            // var stage = stagesScript.stages[current - 1];

            // for (int i = 0; i < stage.totalSpawnLocation; i++)
            // {
            //     Vector2 spawnPos = stage.spawnPositions[i].transform.position;
            //     Instantiate(doorPrefab, spawnPos, Quaternion.Euler(0f, 0f, 90f), spawnParent.transform);
            // }
        }
    }

    public void DecreaseType()
    {
        if (obesityType > 0)
        {
            Debug.Log("Decreasing Obesity Type");
            obesityType--;
        }
        else
        {
            
        }
    }

    public void CheckObesityType()
    {
        switch (obesityType)
        {
            case 1:
                Debug.Log($"Obesity Type: {obesityType}");
                playerInputScript.speed--;
                return;
            case 2:
                Debug.Log($"Obesity Type: {obesityType}");
                playerInputScript.speed--;
                return;
            case 3:
                Debug.Log($"Obesity Type: {obesityType}");
                playerInputScript.speed--;
                return;
            case 4:
                Debug.Log($"Obesity Type: {obesityType}");
                playerInputScript.speed--;
                return;
            case 5:
                Debug.Log($"Obesity Type: {obesityType}");
                gameOverScript.gameOver();
                return;
        }
    }
}
