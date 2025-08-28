using UnityEngine;

public class BiscuitScript : MonoBehaviour, IInteractable
{
    ScoringScript scoringScript;
    ObesityScript obesityScript;

    public int biscuitsValue;

    void Start()
    {
        scoringScript = FindAnyObjectByType<ScoringScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();  
    }

    public void Interact()
    {
        Debug.Log($"Added {biscuitsValue}");
        scoringScript.AddScore(biscuitsValue);
        obesityScript.AddType();
        Destroy(gameObject);
    }
}
