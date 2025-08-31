using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FemaleRatScript : MonoBehaviour, IInteractable
{
    public bool isConversationActivated = false;
    public bool hasAlreadyBought = false;
    public GameObject femaleConversationPanel;
    public TMP_Text femaleConversationText;
    public Button yesButton;
    public Button noButton;

    public int[] costToBuyRevive;

    StagesScript stagesScript;
    GameOverScript gameOverScript;
    ObesityScript obesityScript;

    void Start()
    {
        stagesScript = FindAnyObjectByType<StagesScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
        gameOverScript = FindAnyObjectByType<GameOverScript>();
        yesButton.onClick.AddListener(boughtRevive);
        noButton.onClick.AddListener(rejectOffer);
    }

    public void Interact()
    {
        // Talk, if the player wants to revive or not
        femaleConversationPanel.SetActive(true);
        femaleConversationText.text = "You need some safety?";
        isConversationActivated = true;
    }

    public void boughtRevive()
    {
        // if already have revive, return and cannot buy anymore
        if (gameOverScript.hasRevive || hasAlreadyBought)
        {
            Debug.Log("already has a revive!");
            closePanel();
            return;
        }
        if (!canBuy())
        {
            Debug.Log("Not enough money!");
            closePanel();
            return;
        }

        // else take money from the players
        Debug.Log("Just bought a revive");
        hasAlreadyBought = true;
        gameOverScript.hasRevive = true;
        obesityScript.collectedBiscuits -= costToBuyRevive[stagesScript.currentStageLevel - 1];
        closePanel();
    }

    public void rejectOffer()
    {
        Debug.Log("You dare reject me!");
        closePanel();
    }

    public void closePanel()
    {
        femaleConversationPanel.SetActive(false);
        isConversationActivated = false;
    }

    bool canBuy()
    {
        return obesityScript.collectedBiscuits >= costToBuyRevive[stagesScript.currentStageLevel - 1]; 
    }
}
