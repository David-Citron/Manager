using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grill : MonoBehaviour
{
    public RawImage grillButtonImage;
    public Color32 grillButtonHighlightColor;
    private Color32 grillButtonDefaultColor;
    public Player player;
    public Transform grillPositionPoint;
    public ActionButton grillButton;

    public GameObject sliderMenu;
    public Image progressSliderFill;
    public Slider progressSlider;
    public Color32 rawColor;
    public Color32 cookedColor;
    public Color32 burnedColor;

    // Cooking = true -> Meat is currently cooking otherwise there is no meat cooking
    public bool cooking = false;

    // Cooking state says if meat is cooked, raw or burned
    public string cookingState = "none";
    // 0. none
    // 1. Raw
    // 2. Cooked
    // 3. Burned

    bool continueCooking = false;

    // Start is called before the first frame update
    void Start()
    {
        grillButtonDefaultColor = grillButtonImage.color;
        cookingState = "none";
        cooking = false;
        progressSlider.minValue = 0;
        progressSlider.maxValue = 100;
        ResetSlider();
        SetSliderActive(false);
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

        // Start cooking coroutine
        StartCoroutine(CookingProcess());
    }

    void PlayerTakesMeatFromGrill()
    {
        player.AddItemToInventory(cookingState + "Meat");
        cookingState = "none";
        cooking = false;
        continueCooking = false;
        SetSliderActive(false);
    }

    IEnumerator CookingProcess()
    {
        // Progress range is 0-100 (in percents)
        int progress = 0;

        cookingState = "Raw";
        cooking = true;
        SetSliderActive(true);
        ResetSlider();

        while (progress < 100 && continueCooking)
        {
            yield return new WaitForSecondsRealtime(.2f);
            progress++;
            UpdateSlider(progress);
            if(progress >= 50)
            {
                cookingState = "Cooked";
            }
        }
        if(continueCooking)
        {
            cookingState = "Burned";
        }
    }

    void ResetSlider()
    {
        progressSlider.value = progressSlider.minValue;
        progressSliderFill.color = rawColor;
    }

    void UpdateSlider(int value)
    {
        progressSlider.value = value;
        if(value < 50)
        {
            progressSliderFill.color = rawColor;
        }
        else if(value < 100)
        {
            progressSliderFill.color = cookedColor;
        }
        else
        {
            progressSliderFill.color = burnedColor;
        }
    }

    void SetSliderActive(bool value)
    {
        sliderMenu.SetActive(value);
    }
}
