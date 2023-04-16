using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashRegister : MonoBehaviour
{
    public RawImage cashRegisterButtonImage;
    public Color32 cashRegisterButtonHighlightColor;
    private Color32 cashRegisterButtonDefaultColor;
    public Player player;
    public Transform cashRegisterPositionPoint;
    public ActionButton cashRegisterButton;

    // Start is called before the first frame update
    void Start()
    {
        cashRegisterButtonDefaultColor = cashRegisterButtonImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("CashRegister") > 0)
        {
            Interaction();
        }else if(cashRegisterButton.buttonPressed)
        {
            Interaction();
        }else if(!cashRegisterButton.buttonPressed)
        {
            cashRegisterButtonImage.color = cashRegisterButtonDefaultColor;
        }
    }

    public void Interaction()
    {
        cashRegisterButtonImage.color = cashRegisterButtonHighlightColor;
        player.GoToPosition(cashRegisterPositionPoint.transform.position);
    }
}
