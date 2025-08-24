using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BiscuitScript : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("I just ate this shit nigga!");
    }
}
