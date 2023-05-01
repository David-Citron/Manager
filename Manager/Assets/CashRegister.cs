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

    public Transform customerPoint;
    public bool customerIsAtTheCashRegister;
    public bool customerArrived;
    public Table currentTable;
    public List<Table> queueList;

    // Start is called before the first frame update
    void Start()
    {
        cashRegisterButtonDefaultColor = cashRegisterButtonImage.color;
        customerIsAtTheCashRegister = false;
        customerArrived = false;
        currentTable = null;
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

    public void CustomerGoingToRegister()
    {
        customerIsAtTheCashRegister = true;
    }

    public void CustomerLeavingCashRegister()
    {
        customerIsAtTheCashRegister = false;
    }

    public void JoinQueue(Table other)
    {
        queueList.Add(other);
        QueueUpdate();
    }

    public void RemoveFromQueue(int index)
    {
        queueList.RemoveAt(index);
        StartCoroutine(QueueUpdateDelay());
    }

    public void RemoveFromQueue(Table other)
    {
        queueList.Remove(other);
        StartCoroutine(QueueUpdateDelay());
    }

    public IEnumerator QueueUpdateDelay()
    {
        yield return new WaitForSecondsRealtime(4);
        QueueUpdate();
    }

    public void QueueUpdate()
    {
        queueList.Sort();
        if (!customerIsAtTheCashRegister && queueList.ToArray().Length != 0)
        {
            CustomerGoingToRegister();
            queueList[0].SentCustomerToPay();
            currentTable = queueList[0];
            queueList.RemoveAt(0);
        }
    }

    public void CustomerArriving()
    {
        customerArrived = true;
    }

    public bool IsCustomerAtTheCashRegister()
    {
        return customerArrived;
    }

    public void CustomerPayingAtTheCashRegister()
    {
        // Add money
        currentTable.MakeAllCustomersLeave();
        ResetCashRegisterState();
    }

    public void ResetCashRegisterState()
    {
        customerIsAtTheCashRegister = false;
        customerArrived = false;
        currentTable = null;
        StartCoroutine(QueueUpdateDelay());
    }
}
