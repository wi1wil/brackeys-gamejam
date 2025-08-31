using UnityEngine;

public class BiscuitScript : MonoBehaviour, Biscuit
{
    ScoringScript scoringScript;
    ObesityScript obesityScript;

    public int biscuitsValue;

    void Start()
    {
        scoringScript = FindAnyObjectByType<ScoringScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
    }

    public void Eat()
    {
        Debug.Log("Just ate this shit nigga");
        obesityScript.EatenBiscuits();
        Destroy(gameObject);
    }

    public void Collect()
    {
        if (obesityScript.maxReached)
        {
            Debug.Log("Pockets full chigga");
            return;
        }

        Debug.Log("Just collected this shit nigga");
        obesityScript.CollectedBiscuits();
        Destroy(gameObject);
    }
}