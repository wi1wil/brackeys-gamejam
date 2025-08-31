using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObesityScript : MonoBehaviour
{
    public int obesityType = 0;
    public int overEatenBiscuits = 0;
    public int eatenBiscuits = 0;
    public int collectedBiscuits = 0;
    public int maxCollected;
    public int[] minimumPass;
    public Image[] obesityBarImage;
    
    public bool isDiarrhea = false;
    public bool hasMinimum = false;
    public bool minimumReached = false;
    public bool maxReached = false;

    public GameObject doorPrefab;
    public GameObject spawnParent;
    public TMP_Text collectedText;
    public TMP_Text eatenText;

    PlayerInputScript playerInputScript;
    BiscuitSpawnerScript biscuitSpawnerScript;
    GameOverScript gameOverScript;
    StagesScript stagesScript;

    void Start()
    {
        biscuitSpawnerScript = FindAnyObjectByType<BiscuitSpawnerScript>();
        stagesScript = FindObjectOfType<StagesScript>();
        gameOverScript = FindObjectOfType<GameOverScript>();
        playerInputScript = FindObjectOfType<PlayerInputScript>();
    }

    public void ResetVariables()
    {
        hasMinimum = false;
        minimumReached = false;
        eatenBiscuits = 0;
        overEatenBiscuits = 0;
    }

    void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        collectedText.text = $"{collectedBiscuits}";
        eatenText.text = $"{eatenBiscuits}";
        for (int i = 0; i < obesityBarImage.Length; i++)
        {
            obesityBarImage[i].fillCenter = (i < obesityType);
        }
    }

    public void EatenBiscuits()
    {
        eatenBiscuits++;
        Debug.Log($"Eated: {overEatenBiscuits}, Total: {eatenBiscuits}");
        if (eatenBiscuits >= minimumPass[stagesScript.currentStageLevel - 1])
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
        }
    }

    public void CollectedBiscuits()
    {
        if (maxCollected == 0)
            maxCollected = (biscuitSpawnerScript.totalAmountToBeSpawned[stagesScript.currentStageLevel - 1] - minimumPass[stagesScript.currentStageLevel-1]) / 2;

        if (maxReached)
            return;

        collectedBiscuits++;
        if (collectedBiscuits >= maxCollected)
        {
            maxReached = true;
            Debug.Log("Pockets too full can only eat them now!");
            return;
        }
    }

    public void DecreaseType()
    {
        if (obesityType > 0)
        {
            Debug.Log("Decreasing Obesity Type!");
            obesityType--;
        }
        else
        {
            Debug.Log("You have no obesity!");
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
