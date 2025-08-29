using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotMachineScript : MonoBehaviour, IInteractable
{
    [Header("Slot Machine Setup")]
    public Sprite[] abilitiesSprites;
    public Image[] slots;

    public GameObject gamblingSite;
    public Button gambleButton;

    public Sprite preSpinSprite;
    public Sprite afterSpinSprite;
    public Image slotMachineImage;

    public Transform pos1;
    public Transform pos2;

    [Header("Pity System")]
    public int amountBeforePity = 4;
    public int pityCount = 0;

    [Header("Costs")]
    public int[] costToGamble;
    public TMP_Text[] pityText;
    public TMP_Text costText;

    [HideInInspector] public bool isGambling = false;
    [HideInInspector] public bool isSpinning = false;

    AbilitiesScript abilitiesScript;
    ObesityScript obesityScript;
    StagesScript stagesScript;

    void Start()
    {
        stagesScript = FindAnyObjectByType<StagesScript>();
        obesityScript = FindAnyObjectByType<ObesityScript>();
        abilitiesScript = FindAnyObjectByType<AbilitiesScript>();

        UpdateText();
        ResetPity();
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
                    if (!isSpinning && CanGamble())
                    {
                        StartCoroutine(SlotMachine());
                    }
                    else if (!CanGamble())
                    {
                        Debug.Log("Not enough biscuits!");
                    }
                    else
                    {
                        Debug.Log("Still spinning!");
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

    public bool CanGamble()
    {
        return obesityScript.collectedBiscuits >= costToGamble[stagesScript.currentStageLevel - 1];
    }

    IEnumerator SlotMachine()
    {
        isSpinning = true;
        obesityScript.collectedBiscuits -= costToGamble[stagesScript.currentStageLevel - 1];

        slotMachineImage.sprite = afterSpinSprite;
        gambleButton.transform.position = pos2.position;
        UpdateText();

        Coroutine spinning = StartCoroutine(RandomSpin());

        yield return new WaitForSeconds(5f);
        StopCoroutine(spinning);

        if (pityCount > amountBeforePity)
        {
            Debug.Log("Pity gotten!");
            int guaranteed = Random.Range(0, abilitiesSprites.Length);
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].sprite = abilitiesSprites[guaranteed];
                yield return new WaitForSeconds(0.3f);
            }

            Debug.Log(guaranteed);
            abilitiesScript.AddAbilityCharge(guaranteed);
            yield return StartCoroutine(ResetPity());
            yield break;
        }

        yield return StartCoroutine(AddPity());

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
            abilitiesScript.AddAbilityCharge(abilityIndex);
            yield return StartCoroutine(ResetPity());
        }

        CleanupSpin();
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

    IEnumerator ResetPity()
    {
        Debug.Log("Resetting Pity");
        foreach (var txt in pityText)
        { 
            txt.text = "0";
            yield return new WaitForSeconds(0.2f);
        }
        pityCount = 0;

        CleanupSpin();
        yield break;
    }

    IEnumerator AddPity()
    {
        Debug.Log("Adding Pity");
        if (pityCount < pityText.Length)
        {
            pityText[pityCount].text = "7";
        }
        pityCount++;
        yield break;
    }

    void CleanupSpin()
    {
        slotMachineImage.sprite = preSpinSprite;
        gambleButton.transform.position = pos1.position;
        isSpinning = false;
    }
}