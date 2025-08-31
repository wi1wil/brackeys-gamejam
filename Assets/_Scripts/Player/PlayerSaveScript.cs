using UnityEngine;

public class PlayerSaveScript : MonoBehaviour
{
    ObesityScript obesityScript;
    SurvivalTimeScript survivalTimeScript;

    public int mostCookiesEaten;
    public int mostCookiesCollected;
    public float fastestSurvivalTime;
    public float previousSurvivalTime;

    void Start()
    {
        obesityScript = FindAnyObjectByType<ObesityScript>();
        survivalTimeScript = FindAnyObjectByType<SurvivalTimeScript>();
    }

    public void getVariables()
    {
        mostCookiesCollected = obesityScript.collectedBiscuits;
        mostCookiesEaten = obesityScript.eatenBiscuits;

        int prevSavedCC = PlayerPrefs.GetInt("mostCookiesCollected");
        int prevSavedCE = PlayerPrefs.GetInt("mostCookiesEaten");
        if (mostCookiesCollected > prevSavedCC)
        {
            mostCookiesCollected = obesityScript.collectedBiscuits;
        }
        else
        {
            mostCookiesCollected = prevSavedCC;
        }

        if (mostCookiesEaten > prevSavedCE)
        {
            mostCookiesEaten = obesityScript.eatenBiscuits;
        }
        else
        {
            mostCookiesEaten = prevSavedCE;
        }


        previousSurvivalTime = survivalTimeScript.totalSurvivalTime;
        float prevSavedFastest = PlayerPrefs.GetFloat("fastestSurvivalTime", Mathf.Infinity);
        if (previousSurvivalTime < prevSavedFastest)
        {
            fastestSurvivalTime = previousSurvivalTime;
        }
        else
        {
            fastestSurvivalTime = prevSavedFastest;
        }
    }

    public void SaveData()
    {
        getVariables();
        PlayerPrefs.SetInt("mostCookiesEaten", mostCookiesEaten);
        PlayerPrefs.SetInt("mostCookiesCollected", mostCookiesCollected);
        PlayerPrefs.SetFloat("prevSurvivalTime", previousSurvivalTime);
        PlayerPrefs.SetFloat("fastestSurvivalTime", fastestSurvivalTime);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        mostCookiesEaten = PlayerPrefs.GetInt("mostCookiesEaten", 0);
        mostCookiesCollected = PlayerPrefs.GetInt("mostCookiesCollected", 0);
        previousSurvivalTime = PlayerPrefs.GetFloat("prevSurvivalTime", 0f);
        fastestSurvivalTime = PlayerPrefs.GetFloat("fastestSurvivalTime", Mathf.Infinity);
    }
}
