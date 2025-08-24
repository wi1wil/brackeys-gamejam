using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth = 0;

    public GameObject healthBar;
    public Material originalMaterial;
    public Material flashMaterial;

    private GameOverScript gameOverScript;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        healthBar = GameObject.Find("HealthBar");
        UpdateHealthBar();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
        gameOverScript = GetComponent<GameOverScript>();
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
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    public void UpdateHealthBar()
    {
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
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.material = originalMaterial;
    }
}
