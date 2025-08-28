using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineScript : MonoBehaviour, IInteractable
{
    public Sprite[] abilitiesSprites;
    public Image[] slots;

    public GameObject gamblingSite;
    public Button gambleButton;

    public bool isGambling = false;
    public bool isSpinning = false;

    AbilitiesScript abilitiesScript;

    void Start()
    {
        abilitiesScript = FindAnyObjectByType<AbilitiesScript>();
    }

    public void Interact()
    {
        if (!gamblingSite.activeSelf)
        {
            isGambling = true;
            gamblingSite.SetActive(true);
            if (gambleButton != null)
            {
                gambleButton.onClick.RemoveAllListeners();
                gambleButton.onClick.AddListener(() =>
                {
                    if (!isSpinning)
                    {
                        StartCoroutine(SlotMachine());
                    }
                    else
                    {
                        Debug.Log("Still spinnin");
                    }
                });
            }
        }
        else if(gamblingSite.activeSelf && !isSpinning)
        {
            isGambling = false;
            gamblingSite.SetActive(false);
        }
    }

    IEnumerator SlotMachine()
    {
        isSpinning = true;

        Coroutine spinning = StartCoroutine(RandomSpin());

        yield return new WaitForSeconds(5f);

        StopCoroutine(spinning);
        isSpinning = false;

        int[] results = new int[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            results[i] = Random.Range(0, abilitiesSprites.Length);
            slots[i].sprite = abilitiesSprites[results[i]];
        }

        if (results[0] == results[1] && results[1] == results[2])
        {
            int abilityIndex = results[0];
            Debug.Log("Won ability index: " + abilityIndex);
            AddAbilityCharge(abilityIndex);
        }
    }

    void AddAbilityCharge(int index)
    {
        switch (index)
        {
            case 1:
                if (abilitiesScript.AbilityACharge == 1)
                {
                    Debug.Log("Already have an A ability charge!");
                }
                else
                {
                    abilitiesScript.AbilityACharge++;
                }
                break;

            case 2:
                if (abilitiesScript.AbilityBCharge == 1)
                {
                    Debug.Log("Already have a B ability charge!");
                }
                else
                {
                    abilitiesScript.AbilityBCharge++;
                }
                break;

            case 3: 
                if (abilitiesScript.AbilityCCharge == 1)
                {
                    Debug.Log("Already have a C ability charge!");
                }
                else
                {
                    abilitiesScript.AbilityCCharge++;
                }
                break;

            case 4:
                if (abilitiesScript.AbilityDCharge == 1)
                {
                    Debug.Log("Already have a D ability charge!");
                }
                else
                {
                    abilitiesScript.AbilityDCharge++;
                }
                break;
        }

        abilitiesScript.UpdateImages();
    }

    IEnumerator RandomSpin()
    {
        while (true)
        {
            for (int i = 0; i < slots.Length; i++)
                slots[i].sprite = abilitiesSprites[Random.Range(0, abilitiesSprites.Length)];

            yield return new WaitForSeconds(0.1f);
        }
    }
} 

