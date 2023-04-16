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

    // Start is called before the first frame update
    void Start()
    {
        interactionButtonDefaultColor = interactionButtonImage.color;
    }

    void Update()
    {
        if (Input.GetAxis("Interaction") > 0)
        {
            Interaction();
        }
        else if (interactionButton.buttonPressed)
        {
            Interaction();
        }
        else if (!interactionButton.buttonPressed)
        {
            interactionButtonImage.color = interactionButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        interactionButtonImage.color = interactionButtonHighlightColor;
        player.Interaction();
    }
}
