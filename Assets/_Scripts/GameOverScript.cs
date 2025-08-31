using System.Collections;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public bool hasRevive = false;
    public bool isAlive = true;

    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;
    PlayerSaveScript playerSaveScript;

    void Start()
    {
        playerSaveScript = FindAnyObjectByType<PlayerSaveScript>();
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
            hasRevive = false;
            return;
        }
        playerSaveScript.SaveData();
        Debug.Log("You died!");
        isAlive = false;
    }

    public void playerWon()
    {
        Debug.Log("Player won!");
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
