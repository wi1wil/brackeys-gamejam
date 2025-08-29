using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth = 3;
    public bool isMaxHealth = false;
    public int currentHealth = 0;
    public bool isInvincible = false;

    public GameObject healthBar;
    public Material originalMaterial;
    public Material flashMaterial;

    private GameOverScript gameOverScript;
    private SpriteRenderer spriteRenderer;
    AbilitiesScript abilitiesScript;

    void Start()
    {
        healthBar = GameObject.Find("HealthBar");
        UpdateHealthBar();
        abilitiesScript = FindAnyObjectByType<AbilitiesScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        gameOverScript = FindAnyObjectByType<GameOverScript>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        UpdateHealthBar();
    }

    public void TakeDamage()
    {
        currentHealth--;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Debug.Log("You died!");
            gameOverScript.gameOver();
        }
        StartCoroutine(DamageFlash());
    }

    public void AddHealth()
    {
        if (currentHealth == 3)
        {
            Debug.Log("Max health");
            return;
        }
        currentHealth++;
        StartCoroutine(DamageFlash());
        // Add slurp juice sound here or anythin
    }

    public void FlashEffect()
    {
        StartCoroutine(DamageFlash());
    }

    public void UpdateHealthBar()
    {
        if (currentHealth == 3) isMaxHealth = true;
        
        List<Image> children = new List<Image>();
        foreach (Transform child in healthBar.transform)
        {
            Image img = child.GetComponent<Image>();
            if (img != null)
            {
                children.Add(img);
            }
        }

        for (int i = 0; i < maxHealth; i++)
        {
            children[i].fillCenter = false;
        }

        for (int i = 0; i < currentHealth; i++)
        {
            children[i].fillCenter = true;
        }
    }

    IEnumerator DamageFlash()
    {
        if (isInvincible)
        {
            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.material = flashMaterial;
                yield return new WaitForSeconds(0.25f);
                spriteRenderer.material = originalMaterial;
                yield return new WaitForSeconds(0.25f);
            }
        }

        
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material = originalMaterial;
    }
}
