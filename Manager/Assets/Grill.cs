using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grill : MonoBehaviour
{
    public RawImage grillButtonImage;
    public Color32 grillButtonHighlightColor;
    private Color32 grillButtonDefaultColor;
    public Player player;
    public Transform grillPositionPoint;
    public ActionButton grillButton;

    public GameObject timerMenu;
    public Image timerFill;
    public TMP_Text timerText;
    public RawImage timerBackground;

    public Color32 rawColor;
    public Color32 cookedColor;
    public Color32 burnedColor;
    public Color32 firstTimerStage;
    public Color32 secondTimerStage;
    public Color32 timerFinished;
    public Color32 primaryTimerTextColor;
    public Color32 secondaryTimerTextColor;

    // Cooking = true -> Meat is currently cooking otherwise there is no meat cooking
    public bool cooking = false;

    // Cooking state says if meat is cooked, raw or burned
    public string cookingState = "none";
    // 0. none
    // 1. Raw
    // 2. Cooked
    // 3. Burned

    bool continueCooking = false;

    // MeatMaterial contains materials of different stages of cooking
    public Material[] meatMaterials;
    // 0. Raw
    // 1. Cooked
    // 2. Burned

    public GameObject meatModel;

    // Start is called before the first frame update
    void Start()
    {
        grillButtonDefaultColor = grillButtonImage.color;
        cookingState = "none";
        cooking = false;
        ResetTimer();
        SetTimerVisible(false);
        meatModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Grill") > 0)
        {
            Interaction();
        }
        else if (grillButton.buttonPressed)
        {
            Interaction();
        }
        else if (!grillButton.buttonPressed)
        {
            grillButtonImage.color = grillButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        grillButtonImage.color = grillButtonHighlightColor;
        player.GoToPosition(grillPositionPoint.transform.position);
    }

    public void PlayerInteractionRequest(string itemInInventory)
    {
        if (cooking && itemInInventory == "none" && (cookingState == "Cooked" || cookingState == "Burned"))
        {
            PlayerTakesMeatFromGrill();
        }
        else if(!cooking && itemInInventory == "RawMeat")
        {
            PlayerPutsMeatOnGrill();
        }
    }

    void PlayerPutsMeatOnGrill()
    {
        player.RemoveItemFromInventory();
        continueCooking = true;
        meatModel.SetActive(true);
        meatModel.GetComponent<Renderer>().material = meatMaterials[0];

        // Start cooking coroutine
        StartCoroutine(CookingProcess());
    }

    void PlayerTakesMeatFromGrill()
    {
        player.AddItemToInventory(cookingState + "Meat");
        cookingState = "none";
        cooking = false;
        continueCooking = false;
        SetTimerVisible(false);
        meatModel.SetActive(false);
    }

    IEnumerator CookingProcess()
    {
        // Progress range is 0-100 (in percents)
        int progress = 0;

        cookingState = "Raw";
        cooking = true;
        float updateInterval = .2f;
        float secondsLeft = updateInterval * 100 / 2;
        bool alreadyCooked = false;

        SetTimerVisible(true);
        ResetTimer();

        while (progress < 100 && continueCooking)
        {
            yield return new WaitForSecondsRealtime(updateInterval);
            progress++;
            if(progress == 50)
            {
                cookingState = "Cooked";
                alreadyCooked = true;
                secondsLeft = updateInterval * 100 / 2;
                meatModel.GetComponent<Renderer>().material = meatMaterials[1];
            }
            secondsLeft -= updateInterval;
            // Alternative (less smooth) version for secondsLeft: (int)Mathf.Round(secondsLeft)
            UpdateTimer(progress, (int) secondsLeft, alreadyCooked);
        }
        if(continueCooking)
        {
            cookingState = "Burned";
            meatModel.GetComponent<Renderer>().material = meatMaterials[2];
        }
    }

    void ResetTimer()
    {
        timerFill.color = rawColor;
        timerFill.fillAmount = 0;
        timerBackground.color = firstTimerStage;
        timerText.text = "9"; // Alternative (less smooth) version: "10"
        timerBackground.gameObject.SetActive(true);
        timerText.color = primaryTimerTextColor;
    }

    void UpdateTimer(int valueInPercents, int secondsLeft, bool burningStage)
    {
        timerFill.color = rawColor;
        timerText.text = secondsLeft.ToString();
        timerFill.fillAmount = (float)valueInPercents / 100;

        if(burningStage)
        {
            timerBackground.color = secondTimerStage;
        }
        else
        {
            timerBackground.color = firstTimerStage;
        }

        if(valueInPercents == 100)
        {
            timerFill.color = burnedColor;
            timerBackground.gameObject.SetActive(false);
            timerText.text = "!!!";
            timerText.color = secondaryTimerTextColor;
        }
        else if(valueInPercents >= 50)
        {
            timerFill.color = cookedColor;
        }

        
    }

    void SetTimerVisible(bool value)
    {
        timerMenu.SetActive(value);
    }
}
