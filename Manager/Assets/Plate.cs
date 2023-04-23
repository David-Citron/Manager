using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plate : MonoBehaviour
{
    public RawImage plateButtonImage;
    public Color32 plateButtonHighlightColor;
    private Color32 plateButtonDefaultColor;
    public Player player;
    public Transform platePositionPoint;
    public ActionButton plateButton;

    public bool plateSetup;
    public GameObject plateModel;
    public List<string> ingrediences = new List<string>();

    // Shows different stages of food on the plate
    public GameObject[] plateFoodModels;
    // 0. none
    // 1. ClassicBurger

    // Start is called before the first frame update
    void Start()
    {
        plateButtonDefaultColor = plateButtonImage.color;
        AddPlate();
        UpdatePlateFoodModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Plate") > 0)
        {
            Interaction();
        }
        else if (plateButton.buttonPressed)
        {
            Interaction();
        }
        else if (!plateButton.buttonPressed)
        {
            plateButtonImage.color = plateButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        plateButtonImage.color = plateButtonHighlightColor;
        player.GoToPosition(platePositionPoint.transform.position);
    }

    // Adds item on the plate
    public void AddingItemOnThePlate(string itemName)
    {
        ingrediences.Add(itemName);
        UpdatePlateFoodModel();
    }

    // Returns true if item (itemName) can be added on the plate
    public bool CanAddThisItemOnThePlate(string itemName)
    {
        if(itemName == "CookedMeat" && !ingrediences.Contains("CookedMeat"))
        {
            return true;
        }
        return false;
    }

    // Returns true if plate model is active
    public bool IsPlateSetUp()
    {
        return plateSetup;
    }

    // Adds plate model
    public void AddPlate()
    {
        plateSetup = true;
        plateModel.SetActive(true);
        ingrediences.Clear();
        UpdatePlateFoodModel();
    }

    // Removes plate model
    public void RemovePlate()
    {
        plateSetup = false;
        plateModel.SetActive(false);
    }

    // Returns true if player can right now take the plate
    public bool CanCurrentlyTakeThePlate()
    {
        string currentItemName = "none";

        if (ingrediences.Contains("CookedMeat"))
        {
            currentItemName = "ClassicBurger";
        }

        if(currentItemName != "none")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Removes plate and returns name of an item created on the plate
    public string TakePlate()
    {
        string currentItemName = "none";
        if(ingrediences.Contains("CookedMeat"))
        {
            currentItemName = "ClassicBurger";
        }
        RemovePlate();
        return currentItemName;
    }

    public void UpdatePlateFoodModel()
    {
        if(!IsPlateSetUp())
        {
            return;
        }
        int indexOfCurrentState = 0;
        if(ingrediences.Contains("CookedMeat"))
        {
            indexOfCurrentState = 1;
        }
        for (int i = 0; i < plateFoodModels.Length; i++)
        {
            if(i == indexOfCurrentState)
            {
                plateFoodModels[i].SetActive(true);
            }else
            {
                plateFoodModels[i].SetActive(false);
            }
        }
    }
}
