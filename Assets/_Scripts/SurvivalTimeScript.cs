using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class SurvivalTimeScript : MonoBehaviour
{
    public float totalSurvivalTime;
    public float[] currentStageSurvivalTime;
    public TMP_Text[] stageSurvivalTimeText;
    public TMP_Text totalSurivalTimeText;

    public bool isFinal = false;
    public bool isTimerActive = false;

    StagesScript stagesScript;
    GameOverScript gameOverScript;

    void Start()
    {
        stagesScript = FindAnyObjectByType<StagesScript>();
        gameOverScript = FindAnyObjectByType<GameOverScript>();
    }

    void Update()
    {
        if (isTimerActive && gameOverScript.isAlive)
        {
            currentStageSurvivalTime[stagesScript.currentStageLevel - 1] += Time.deltaTime;
            UpdateUI();
        }
    }

    public void FinalizeSurvivalTime()
    {
        totalSurvivalTime = 0;
        for (int i = 0; i < currentStageSurvivalTime.Length; i++)
        {
            totalSurvivalTime += currentStageSurvivalTime[i];
        }

        int minutes = Mathf.FloorToInt(totalSurvivalTime / 60);
        int seconds = Mathf.FloorToInt(totalSurvivalTime % 60);
        totalSurivalTimeText.text = $"Total: {minutes:00}:{seconds:00}";

        Debug.Log($"Player died, total survival time is {minutes:00}:{seconds:00}");
        isFinal = true;
    }

    void UpdateUI()
    {
        int currentStage = stagesScript.currentStageLevel - 1;
        stageSurvivalTimeText[currentStage].gameObject.SetActive(true);
        if (currentStage >= 1)
        {
            stageSurvivalTimeText[currentStage - 1].gameObject.SetActive(false);
        }
        int minutes = Mathf.FloorToInt(currentStageSurvivalTime[currentStage] / 60);
        int seconds = Mathf.FloorToInt(currentStageSurvivalTime[currentStage] % 60);
        stageSurvivalTimeText[currentStage].text = $"({minutes:00}:{seconds:00})";
    }
}
