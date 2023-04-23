using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rack : MonoBehaviour
{
    public RawImage rackButtonImage;
    public Color32 rackButtonHighlightColor;
    private Color32 rackButtonDefaultColor;
    public Player player;
    public Transform rackPositionPoint;
    public ActionButton rackButton;

    public int maxSupplies = 32;
    public int minSupplies = 0;
    public int currentSupplies;

    public GameObject[] boxModels;
    public int numberOfBoxesAvailable = 8;

    public TMP_Text suppliesLabel;

    // Start is called before the first frame update
    void Start()
    {
        rackButtonDefaultColor = rackButtonImage.color;
        if(maxSupplies <= minSupplies)
        {
            maxSupplies = minSupplies + 1;
        }
        currentSupplies = maxSupplies;
        numberOfBoxesAvailable = boxModels.Length;
        UpdateSupplies();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Rack") > 0)
        {
            Interaction();
        }
        else if (rackButton.buttonPressed)
        {
            Interaction();
        }
        else if (!rackButton.buttonPressed)
        {
            rackButtonImage.color = rackButtonDefaultColor;
        }

        UpdateSupplies();
    }

    public void Interaction()
    {
        rackButtonImage.color = rackButtonHighlightColor;
        player.GoToPosition(rackPositionPoint.transform.position);
    }

    public bool CanTakeSupplies()
    {
        if(currentSupplies > minSupplies)
        {
            return true;
        }
        return false;
    }

    public void TakeSupplies()
    {
        currentSupplies--;
        UpdateSupplies();
    }

    public void UpdateSupplies()
    {
        suppliesLabel.text = currentSupplies + "/" + maxSupplies;
        showCurrentBoxModels();
    }

    private void showCurrentBoxModels()
    {
        int numberOfBoxesToShow = ((currentSupplies + (maxSupplies / numberOfBoxesAvailable - 1)) / (maxSupplies / numberOfBoxesAvailable));
        showNumberOfBoxes(numberOfBoxesToShow);
    }
    
    private void showNumberOfBoxes(int numberOfBoxes)
    {
        if(numberOfBoxes > maxSupplies)
        {
            numberOfBoxes = maxSupplies;
        }
        for (int i = 0; i < numberOfBoxesAvailable; i++)
        {
            if((i+1) <= numberOfBoxes)
            {
                boxModels[i].SetActive(true);
            }else
            {
                boxModels[i].SetActive(false);
            }
        }
    }
}
