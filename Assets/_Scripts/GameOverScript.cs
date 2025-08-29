using System;
using System.Collections;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public bool hasRevive = false;

    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;

    void Start()
    {
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
    }

    public void gameOver()
    {
        if (hasRevive)
        {
            Debug.Log("The guardian angle has revived and saved you!");
            playerHealthScript.AddHealth();
            StartCoroutine(invincibilityRevive());
        }
        Debug.Log("You died!");
    }

    IEnumerator invincibilityRevive()
    {
        Debug.Log("Invincibility activated!");
        playerInputScript.gameObject.layer = LayerMask.NameToLayer("Default");
        playerHealthScript.isInvincible = true;
        playerHealthScript.FlashEffect();
        yield return new WaitForSeconds(5f);
        playerHealthScript.isInvincible = false;
        playerInputScript.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
