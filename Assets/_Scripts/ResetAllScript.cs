using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllScript : MonoBehaviour
{
    public void resetGameVariables()
    {
        ObesityScript obesityScript = FindAnyObjectByType<ObesityScript>();
        if (obesityScript != null)
        {
            obesityScript.obesityType = 0;
            obesityScript.overEatenBiscuits = 0;
            obesityScript.eatenBiscuits = 0;
            obesityScript.collectedBiscuits = 0;
            obesityScript.maxCollected = 0;
            obesityScript.hasMinimum = false;
            obesityScript.minimumReached = false;
            obesityScript.maxReached = false;
            obesityScript.isDiarrhea = false;
            obesityScript.UpdateText();
        }

        PlayerHealthScript playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        if (playerHealthScript != null)
        {
            playerHealthScript.currentHealth = playerHealthScript.maxHealth;
            playerHealthScript.isMaxHealth = false;
            playerHealthScript.isInvincible = false;
            playerHealthScript.UpdateHealthBar();
        }

        StagesScript stagesScript = FindAnyObjectByType<StagesScript>();
        if (stagesScript != null)
        {
            stagesScript.currentStageLevel = 1;
            PlayerInputScript playerInputScript = FindAnyObjectByType<PlayerInputScript>();
            if (playerInputScript != null && stagesScript.playerSpawnLocations != null && stagesScript.playerSpawnLocations.Length > 0)
            {
                playerInputScript.transform.position = stagesScript.playerSpawnLocations[0].position;
            }
            if (stagesScript.confiner != null && stagesScript.cameraBounds != null && stagesScript.cameraBounds.Length > 0)
            {
                stagesScript.confiner.m_BoundingShape2D = stagesScript.cameraBounds[0];
                stagesScript.confiner.InvalidateCache();
            }
        }

        FemaleRatScript femaleRatScript = FindAnyObjectByType<FemaleRatScript>();
        if (femaleRatScript != null)
        {
            femaleRatScript.hasAlreadyBought = false;
        }

        GameOverScript gameOverScript = FindAnyObjectByType<GameOverScript>();
        if (gameOverScript != null)
        {
            gameOverScript.hasRevive = false;
            gameOverScript.isAlive = true;
        }

        SurvivalTimeScript survivalTimeScript = FindAnyObjectByType<SurvivalTimeScript>();
        if (survivalTimeScript != null)
        {
            survivalTimeScript.totalSurvivalTime = 0;
            survivalTimeScript.isFinal = false;
            for (int i = 0; i < survivalTimeScript.currentStageSurvivalTime.Length; i++)
            {
                survivalTimeScript.currentStageSurvivalTime[i] = 0;
                survivalTimeScript.stageSurvivalTimeText[i].gameObject.SetActive(false);
                survivalTimeScript.stageSurvivalTimeText[i].text = "(00:00)";
            }
            survivalTimeScript.totalSurivalTimeText.text = "Total: 00:00";
        }

        SlotMachineScript slotMachineScript = FindAnyObjectByType<SlotMachineScript>();
        if (slotMachineScript != null)
        {
            slotMachineScript.pityCount = 0;
            slotMachineScript.isGambling = false;
            slotMachineScript.isSpinning = false;
            slotMachineScript.gamblingSite.SetActive(false);
            slotMachineScript.UpdateText();
        }

        DiarrheaPillsScript diarrheaPillsScript = FindAnyObjectByType<DiarrheaPillsScript>();
        if (diarrheaPillsScript != null)
        {
            diarrheaPillsScript.confirmationPanel.SetActive(false);
            diarrheaPillsScript.textPanel.SetActive(false);
            diarrheaPillsScript.hasTakenOnce = false;
            diarrheaPillsScript.confirmationPanelOpened = false;
        }

    }
}
