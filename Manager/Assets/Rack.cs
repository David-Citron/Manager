using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rack : MonoBehaviour
{
    public RawImage rackButtonImage;
    public Color32 rackButtonHighlightColor;
    private Color32 rackButtonDefaultColor;
    public Player player;
    public Transform rackPositionPoint;
    public ActionButton rackButton;

    // Start is called before the first frame update
    void Start()
    {
        rackButtonDefaultColor = rackButtonImage.color;
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
    }

    public void Interaction()
    {
        rackButtonImage.color = rackButtonHighlightColor;
        player.GoToPosition(rackPositionPoint.transform.position);
    }
}
