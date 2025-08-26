using UnityEngine;
using UnityEngine.UIElements;

public class BiscuitScript : MonoBehaviour, IInteractable
{
    ScoringScript scoringScript;
    ObesityScript obesityScript;

    public int biscuitsValue;

    void Start()
    {
        scoringScript = GetComponent<ScoringScript>();
        obesityScript = GetComponent<ObesityScript>();  
    }

    public void Interact()
    {
        Debug.Log($"Added {biscuitsValue}");
        scoringScript.AddScore(biscuitsValue);
        obesityScript.AddType();
        Destroy(gameObject);
    }
}
