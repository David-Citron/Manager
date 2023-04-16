using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public RawImage tableButtonImage;
    public Color32 tableButtonHighlightColor;
    private Color32 tableButtonDefaultColor;
    public Player player;
    public Transform tablePositionPoint;
    public ActionButton tableButton;
    public string axisName = "Table 1";
    public Transform[] customerPoints;

    // Start is called before the first frame update
    void Start()
    {
        tableButtonDefaultColor = tableButtonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(axisName) > 0)
        {
            Interaction();
        }
        else if (tableButton.buttonPressed)
        {
            Interaction();
        }
        else if (!tableButton.buttonPressed)
        {
            tableButtonImage.color = tableButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        tableButtonImage.color = tableButtonHighlightColor;
        player.GoToPosition(tablePositionPoint.transform.position);
    }
}
