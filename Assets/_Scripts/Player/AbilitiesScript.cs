using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilitiesScript : MonoBehaviour
{
    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;
    ObesityScript obesityScript;

    public int AbilityACharge = 0;
    public int AbilityBCharge = 0;
    public int AbilityCCharge = 0;
    public int AbilityDCharge = 0;

    public Image AbilityA;
    public Image AbilityB;
    public Image AbilityC;
    public Image AbilityD;

    void Start()
    {
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
    }

    void Update()
    {
        UpdateImages();
    }

    public void UpdateImages()
    {
        SetAbilityAlpha(AbilityA, AbilityACharge);
        SetAbilityAlpha(AbilityB, AbilityBCharge);
        SetAbilityAlpha(AbilityC, AbilityCCharge);
        SetAbilityAlpha(AbilityD, AbilityDCharge);
    }

    private void SetAbilityAlpha(Image img, int charges)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = (charges <= 0) ? (100f / 255f) : 1f;
        img.color = c;
    }

    public void OnSpeedBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
            StartCoroutine(SpeedBoost());
    }

    public void OnAddLife(InputAction.CallbackContext context)
    {
        if (context.performed)
            StartCoroutine(AddLife());
    }

    public void OnInvincibility(InputAction.CallbackContext context)
    {
        if(context.performed)
            StartCoroutine(Invincibility());
    }

    public void OnDiarrhea(InputAction.CallbackContext context)
    {
        if(context.performed)
            StartCoroutine(Diarrhea());
    }

    // Increase player speeds for a few seconds 
    IEnumerator SpeedBoost()
    {
        if (AbilityACharge <= 0)
        {
            Debug.Log("No speed charges, get some first!");
            yield break;
        }

        AbilityACharge--;
        UpdateImages();

        Debug.Log("Adding player's speed by 20%");
        playerInputScript.speed += 3;
        yield return new WaitForSeconds(10);
        playerInputScript.speed -= 3;
    }

    // When activated, player cannot be targetted, 
    IEnumerator Invincibility()
    {
        if (AbilityBCharge <= 0)
        {
            Debug.Log("No invincibility charges, get some first!");
            yield break;
        }

        AbilityBCharge--;
        UpdateImages();

        playerInputScript.gameObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("Activating Invincibility");
        playerHealthScript.isInvincible = true;
        playerHealthScript.FlashEffect();
        yield return new WaitForSeconds(5);
        playerHealthScript.isInvincible = false;
        playerInputScript.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator AddLife()
    {
        if (AbilityCCharge <= 0)
        {
            Debug.Log("No health charges!");
            yield break;
        }

        AbilityCCharge--;
        UpdateImages();

        Debug.Log("Adding health");
        playerHealthScript.AddHealth();
        yield return new WaitForSeconds(5);
    }

    // Obesity Neglector, if activated, no obesity levels can be increased in this time frame.
    IEnumerator Diarrhea()
    {
        if (AbilityDCharge <= 0)
        {
            Debug.Log("No diarrhea pills!");
            yield break;
        }

        AbilityDCharge--;
        UpdateImages();

        Debug.Log("Invincibility towards eating added!");
        obesityScript.isDiarrhea = true;
        yield return new WaitForSeconds(30);
        obesityScript.isDiarrhea = false;
    }
}
