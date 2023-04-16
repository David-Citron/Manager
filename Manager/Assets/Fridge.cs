using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{
    public RawImage fridgeButtonImage;
    public Color32 fridgeButtonHighlightColor;
    private Color32 fridgeButtonDefaultColor;
    public Player player;
    public Transform fridgePositionPoint;
    public ActionButton fridgeButton;

    // Start is called before the first frame update
    void Start()
    {
        fridgeButtonDefaultColor = fridgeButtonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fridge") > 0)
        {
            Interaction();
        }
        else if (fridgeButton.buttonPressed)
        {
            Interaction();
        }
        else if (!fridgeButton.buttonPressed)
        {
            fridgeButtonImage.color = fridgeButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        fridgeButtonImage.color = fridgeButtonHighlightColor;
        player.GoToPosition(fridgePositionPoint.transform.position);
    }
}
