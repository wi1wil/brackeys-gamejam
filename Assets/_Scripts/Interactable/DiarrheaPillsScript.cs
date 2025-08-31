using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiarrheaPillsScript : MonoBehaviour
{
    ObesityScript obesityScript;
    GameOverScript gameOverScript;

    public GameObject confirmationPanel;
    public GameObject textPanel;
    public TMP_Text conversationText;
    public Button yesButton;
    public Button noButton;

    public bool hasTakenOnce = false;
    public bool confirmationPanelOpened = false;

    void Start()
    {
        gameOverScript = FindAnyObjectByType<GameOverScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();

        yesButton.onClick.AddListener(YesConfirmation);
        noButton.onClick.AddListener(NoConfirmation);

        confirmationPanel.SetActive(false);
        textPanel.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("Trying to eat a pill...");
        if (obesityScript.obesityType != 0)
        {
            Debug.Log("Decreasing your obesity");
            obesityScript.DecreaseType();
        }
        else
        {
            confirmationPanel.SetActive(true);
            confirmationPanelOpened = true;
        }
    }

    public void YesConfirmation()
    {
        if (!hasTakenOnce)
        {
            hasTakenOnce = true;
            StartCoroutine(DisplayText("You’re lucky this time!"));
        }
        else
        {
            Debug.Log("You died due to overdose!");
            StartCoroutine(DisplayText("You died due to overdose!"));
            gameOverScript.gameOver();
        }
    }

    public void NoConfirmation()
    {
        Debug.Log("You decided not to eat the pill.");
        confirmationPanel.SetActive(false);
        confirmationPanelOpened = false;
    }

    IEnumerator DisplayText(string cnvText)
    {
        confirmationPanel.SetActive(false);
        confirmationPanelOpened = false;

        textPanel.SetActive(true);
        conversationText.text = cnvText;
        yield return new WaitForSeconds(2);
        textPanel.SetActive(false);
    }
}