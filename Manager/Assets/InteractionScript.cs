using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public ActionButton interactionButton;
    public RawImage interactionButtonImage;
    public Color32 interactionButtonHighlightColor;
    private Color32 interactionButtonDefaultColor;
    public Player player;
    private bool pressedLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        interactionButtonDefaultColor = interactionButtonImage.color;
        pressedLastFrame = false;
    }

    void Update()
    {
        if (Input.GetAxis("Interaction") > 0)
        {
            Interaction();
            return;
        }
        if (interactionButton.buttonPressed)
        {
            Interaction();
            return;
        }
        pressedLastFrame = false;
        if (!interactionButton.buttonPressed)
        {
            interactionButtonImage.color = interactionButtonDefaultColor;
            return;
        }
    }

    public void Interaction()
    {
        if(pressedLastFrame)
        {
            return;
        }
        pressedLastFrame = true;
        interactionButtonImage.color = interactionButtonHighlightColor;
        player.Interaction();
    }
}
