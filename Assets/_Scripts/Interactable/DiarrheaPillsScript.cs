using UnityEngine;

public class DiarrheaPillsScript : MonoBehaviour, IInteractable
{
    ObesityScript obesityScript;

    void Start()
    {
        obesityScript = FindAnyObjectByType<ObesityScript>();
    }

    public void Interact()
    {
        Debug.Log("Eating a Pill");
        obesityScript.DecreaseType();
        Destroy(gameObject);
    }
}
