using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteItem : MonoBehaviour
{
    public RawImage deleteItemButtonImage;
    public Color32 deleteItemButtonHighlightColor;
    private Color32 deleteItemButtonDefaultColor;
    public ActionButton deleteItemButton;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        deleteItemButtonDefaultColor = deleteItemButtonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("DeleteItem") > 0)
        {
            Interaction();
        }
        else if (deleteItemButton.buttonPressed)
        {
            Interaction();
        }
        else if (!deleteItemButton.buttonPressed)
        {
            deleteItemButtonImage.color = deleteItemButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        deleteItemButtonImage.color = deleteItemButtonHighlightColor;
        player.RemoveItemFromInventory();
    }
}
