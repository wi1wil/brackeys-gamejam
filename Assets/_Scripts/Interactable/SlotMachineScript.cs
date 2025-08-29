using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;

public class SlotMachineScript : MonoBehaviour, IInteractable
{
    public Sprite[] abilitiesSprites;
    public Image[] slots;

    public GameObject gamblingSite;
    public Button gambleButton;

    public Sprite preSpinSprite;
    public Sprite afterSpinSprite;
    public Image slotMachineImage;

    public Transform pos1;
    public Transform pos2;

    public bool isGambling = false;
    public bool isSpinning = false;
    public bool nextPity = false;

    public int[] costToGamble;
    public TMP_Text costText;

    AbilitiesScript abilitiesScript;
    ObesityScript obesityScript;
    StagesScript stagesScript;

    void Start()
    {
        stagesScript = FindAnyObjectByType<StagesScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
        abilitiesScript = FindAnyObjectByType<AbilitiesScript>();
        UpdateText();
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
                    if (!isSpinning && canGamble())
                    {
                        StartCoroutine(SlotMachine());
                    }
                    else if (!canGamble())
                    {
                        Debug.Log("Not enough currency!");
                    }
                    else
                    {
                        Debug.Log("Still spinnin nigga");
                    }
                });
            }
        }
        else if (gamblingSite.activeSelf && !isSpinning)
        {
            isGambling = false;
            gamblingSite.SetActive(false);
        }
    }

    public bool canGamble()
    {
        return obesityScript.collectedBiscuits >= costToGamble[stagesScript.currentStageLevel-1];
    }

    IEnumerator SlotMachine()
    {
        isSpinning = true;
        obesityScript.collectedBiscuits -= costToGamble[stagesScript.currentStageLevel-1];
        slotMachineImage.sprite = afterSpinSprite;
        gambleButton.transform.position = pos2.position;
        UpdateText();

        Coroutine spinning = StartCoroutine(RandomSpin());

        yield return new WaitForSeconds(5f);

        StopCoroutine(spinning);
        slotMachineImage.sprite = preSpinSprite;
        gambleButton.transform.position = pos1.position;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
    
    public void UpdateText()
    {
        costText.text = "Cost: " + costToGamble[stagesScript.currentStageLevel - 1] + " Cookies!";
    }
} 

