using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObesityScript : MonoBehaviour
{
    public int obesityType = 0;
    public int eatenBiscuits = 0;
    public int totalBiscuits = 0;
    public int minimumPass = 5;
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

    public void AddType()
    {
        
        totalBiscuits++;
        Debug.Log($"Eated: {eatenBiscuits}, Total: {totalBiscuits}");
        if (totalBiscuits >= minimumPass)
        {
            Debug.Log("Minimum Reached");
            hasMinimum = true;
        }

        if (hasMinimum)
        {
            eatenBiscuits++;
        }
        if (eatenBiscuits >= 2 && hasMinimum)
        {
            obesityType++;
            Debug.Log($"Obesity Type: {obesityType}");
            CheckObesityType();
            eatenBiscuits = 0;
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

    public void DecreaseType()
    {
        if (obesityType < 5)
        {
            obesityType--;
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
