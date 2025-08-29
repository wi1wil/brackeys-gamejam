using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillScript : MonoBehaviour, IInteractable
{
    DiarrheaPillsScript diarrheaPillsScript;

    void Start()
    {
        diarrheaPillsScript = FindAnyObjectByType<DiarrheaPillsScript>();
    }

    public void Interact()
    {
        Debug.Log("Trying to eat a pill...");
        diarrheaPillsScript.Interact();
        Destroy(gameObject);
    }
}
