using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilitiesScript : MonoBehaviour
{
    PlayerHealthScript playerHealthScript;
    PlayerInputScript playerInputScript;
    ObesityScript obesityScript;

    [Header("Charges")]
    public int[] abilityCharges = new int[4];

    [Header("UI")]
    public Image[] abilityImages;

    [Header("Durations")]
    public float speedBoostDuration = 10f;
    public float invincibilityDuration = 5f;
    public float diarrheaDuration = 30f;

    void Start()
    {
        playerHealthScript = FindAnyObjectByType<PlayerHealthScript>();
        playerInputScript = FindAnyObjectByType<PlayerInputScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();

        UpdateImages();
    }

    void Update()
    {
        UpdateImages();
    }

    public void UpdateImages()
    {
        for (int i = 0; i < abilityImages.Length; i++)
            SetAbilityAlpha(abilityImages[i], abilityCharges[i]);
    }

    private void SetAbilityAlpha(Image img, int charges)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = (charges <= 0) ? (100f / 255f) : 1f;
        img.color = c;
    }

    public void AddAbilityCharge(int index)
    {
        if (index >= abilityCharges.Length)
        {
            Debug.Log("On max health!");   
        }

        if (abilityCharges[index] == 1)
        {
            Debug.Log($"Already have ability {index} charge!");
            return;
        }
        Debug.Log($"Gotten a ability {abilityCharges[index]}");
        abilityCharges[index]++;
        UpdateImages();
    }

    public void OnSpeedBoost(InputAction.CallbackContext context)
    {
        if (context.performed) StartCoroutine(SpeedBoost());
    }

    public void OnInvincibility(InputAction.CallbackContext context)
    {
        if (context.performed) StartCoroutine(Invincibility());
    }

    public void OnAddLife(InputAction.CallbackContext context)
    {
        if (context.performed) StartCoroutine(AddLife());
    }

    public void OnDiarrhea(InputAction.CallbackContext context)
    {
        if (context.performed) StartCoroutine(Diarrhea());
    }

    IEnumerator SpeedBoost()
    {
        if (UseCharge(0))
        {
            Debug.Log("Speed Boost activated!");
            playerInputScript.speed += 3;
            yield return new WaitForSeconds(speedBoostDuration);
            playerInputScript.speed -= 3;
        }
    }

    IEnumerator AddLife()
    {
        if (playerHealthScript.isMaxHealth) yield break;
        if (UseCharge(1))
        {
            Debug.Log("Adding health");
            playerHealthScript.AddHealth();
            yield return null;
        }
    }

    IEnumerator Invincibility()
    {
        if (UseCharge(2))
        {
            Debug.Log("Invincibility activated!");
            playerInputScript.gameObject.layer = LayerMask.NameToLayer("Default");
            playerHealthScript.isInvincible = true;
            playerHealthScript.FlashEffect();
            yield return new WaitForSeconds(invincibilityDuration);
            playerHealthScript.isInvincible = false;
            playerInputScript.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }


    IEnumerator Diarrhea()
    {
        if (UseCharge(3))
        {
            Debug.Log("Obesity blocked (Diarrhea active)!");
            obesityScript.isDiarrhea = true;
            yield return new WaitForSeconds(diarrheaDuration);
            obesityScript.isDiarrhea = false;
        }
    }

    private bool UseCharge(int index)
    {
        if (abilityCharges[index] <= 0)
        {
            Debug.Log($"No charges for ability {index}!");
            return false;
        }

        abilityCharges[index]--;
        UpdateImages();
        return true;
    }
}