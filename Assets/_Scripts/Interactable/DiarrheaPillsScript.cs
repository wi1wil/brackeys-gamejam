using System.ComponentModel.Design;
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
        if (obesityScript.obesityType > 0)
        {
            Debug.Log("Decreasing your obesity");
            obesityScript.DecreaseType();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You have no obesity, this won't work!");
        }
    }
}
