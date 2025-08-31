using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public bool hasRevive = false;
    public bool isAlive = true;

    public string[] deathMessages;
    public TMP_Text gameOverText;
    public TMP_Text totalScore;
    public TMP_Text timeAlive;
    public Button playAgain;
    public Button startButton;

    public TMP_Text mostCookiesEatenText;
    public TMP_Text mostCookiesCollectedText;
    public TMP_Text fastestSurvivalTimeText;

    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;
    PlayerSaveScript playerSaveScript;
    ResetAllScript resetAllScript;
    ObesityScript obesityScript;
    SurvivalTimeScript survivalTimeScript;

    public GameObject startMenuPanel;
    public GameObject gameOverPanel;

    void Start()
    {
        resetAllScript = FindAnyObjectByType<ResetAllScript>();
        playerSaveScript = FindAnyObjectByType<PlayerSaveScript>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
        survivalTimeScript = FindAnyObjectByType<SurvivalTimeScript>();

        playAgain.onClick.AddListener(restartGame);
        startButton.onClick.AddListener(startGame);
    }

    public void gameOver()
    {
        if (!isAlive) return;

        if (hasRevive)
        {
            Debug.Log("The guardian angel has revived and saved you!");
            playerHealthScript?.AddHealth();
            StartCoroutine(invincibilityRevive());
            hasRevive = false;
            return;
        }

        isAlive = false;
        Time.timeScale = 0;

        survivalTimeScript?.FinalizeSurvivalTime();

        playerSaveScript.SaveData();
        gameOverPanel.SetActive(true);

        if (deathMessages != null && deathMessages.Length > 0)
            gameOverText.text = deathMessages[Random.Range(0, deathMessages.Length)];
        else
            gameOverText.text = "You died!";

        int eaten = obesityScript.eatenBiscuits;
        int collected = obesityScript.collectedBiscuits;
        int total = eaten + collected;

        float survived = survivalTimeScript.totalSurvivalTime;
        int minutes = Mathf.FloorToInt(survived / 60);
        int seconds = Mathf.FloorToInt(survived % 60);
        timeAlive.text = $"Alived for: {minutes:00}:{seconds:00}";

        totalScore.text = $"Score: {total}";

        int mostEaten = PlayerPrefs.GetInt("mostCookiesEaten", 0);
        int mostCollected = PlayerPrefs.GetInt("mostCookiesCollected", 0);
        float bestSurvivalTime = PlayerPrefs.GetFloat("bestSurvivalTime", 0f); 

        if (eaten > mostEaten)
        {
            mostEaten = eaten;
            PlayerPrefs.SetInt("mostCookiesEaten", mostEaten);
        }

        if (collected > mostCollected)
        {
            mostCollected = collected;
            PlayerPrefs.SetInt("mostCookiesCollected", mostCollected);
        }

        if (survived > bestSurvivalTime)
        {
            bestSurvivalTime = survived;
            PlayerPrefs.SetFloat("bestSurvivalTime", bestSurvivalTime);
        }

        PlayerPrefs.Save();

        mostCookiesEatenText.text = $"Cookies Eaten: {mostEaten}";
        mostCookiesCollectedText.text = $"Cookies Collected: {mostCollected}";

        if (bestSurvivalTime <= 0f)
        {
            fastestSurvivalTimeText.text = $"Best Survival Time: --:--";
        }
        else
        {
            int bestMin = Mathf.FloorToInt(bestSurvivalTime / 60);
            int bestSec = Mathf.FloorToInt(bestSurvivalTime % 60);
            fastestSurvivalTimeText.text = $"Best Survival Time: {bestMin:00}:{bestSec:00}";
        }
    }

    public void playerWon()
    {
        Debug.Log("Player won!");
        gameOverText.text = "Successfully Escaped!";
        gameOverPanel.SetActive(true);
        resetAllScript.resetGameVariables();
    }

    public void restartGame()
    {
        gameOverPanel.SetActive(false);
        startMenuPanel.SetActive(true);

        if (resetAllScript != null)
            resetAllScript.resetGameVariables();

        survivalTimeScript.isTimerActive = false;

        isAlive = true; 
        Time.timeScale = 1;
        hasRevive = false;
    }


    public void startGame()
    {
        startMenuPanel.SetActive(false);
        resetAllScript.resetGameVariables();
        
        if (survivalTimeScript != null)
        {
            survivalTimeScript.totalSurvivalTime = 0f; 
            survivalTimeScript.isTimerActive = true;
        }

        isAlive = true;
    }

    IEnumerator invincibilityRevive()
    {
        Debug.Log("Invincibility activated!");

        if (playerInputScript != null)
            playerInputScript.gameObject.layer = LayerMask.NameToLayer("Default");

        if (playerHealthScript != null)
        {
            playerHealthScript.isInvincible = true;
            playerHealthScript.FlashEffect();
        }

        yield return new WaitForSeconds(5f);

        if (playerHealthScript != null)
            playerHealthScript.isInvincible = false;

        if (playerInputScript != null)
            playerInputScript.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}