using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Table : MonoBehaviour
{
    public Image tableButtonImage;
    public Color32 tableButtonHighlightColor;
    private Color32 tableButtonDefaultColor;
    public Player player;
    public Transform tablePositionPoint;
    public ActionButton tableButton;
    public string axisName = "Table 1";
    public int numberOfSeats = 0;

    public Transform[] customerPoints;
    public bool[] seatsUsed;
    public bool[] customersArrived;
    public Customer[] customers;

    public RawImage orderImage;
    public GameObject orderSlot;
    bool forceHideOrderSlot;

    public Image tableClockFill;
    public Color32 tableClockFillColor;
    
    // Names of current item wanted by customers at the table, "none" = there are no customers at the table
    public string nameOfCurrentOrder = "none";
    // 0. none
    // 1. ClassicBurger

    Texture[] orderTextures;
    public TableManagementScript tableManagementScript;

    public GameObject plateModel;
    public GameObject[] models;

    // Start is called before the first frame update
    void Start()
    {
        tableManagementScript = Camera.main.GetComponent<TableManagementScript>();
        tableButtonDefaultColor = tableButtonImage.color;
        numberOfSeats = customerPoints.Length;
        seatsUsed = new bool[numberOfSeats];
        customersArrived = new bool[numberOfSeats];
        customers = new Customer[numberOfSeats];
        for (int i = 0; i < numberOfSeats; i++)
        {
            seatsUsed[i] = false;
        }
        orderTextures = tableManagementScript.orderTextures;
        forceHideOrderSlot = false;
        UpdateOrderImage();
        SetClockFillVisibility(false);
        plateModel.SetActive(false);
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
            tableClockFill.color = tableClockFillColor;
        }
    }

    public void Interaction()
    {
        tableButtonImage.color = tableButtonHighlightColor;
        player.GoToPosition(tablePositionPoint.transform.position);
        tableClockFill.color = tableButtonHighlightColor;
    }

    public void SetClockFillVisibility(bool value)
    {
        tableClockFill.gameObject.SetActive(value);
    }

    public void CustomerSeatRequest(NavMeshAgent customerNavMeshAgent, Customer customer)
    {
        bool allSeatsReserved = true;
        Vector3 firstEmptySeatPosition = new Vector3();
        int seatReservedIndex = 0;
        for (int i = 0; i < numberOfSeats; i++)
        {
            if(seatsUsed[i] == false && allSeatsReserved)
            {
                allSeatsReserved = false;
                seatReservedIndex = i;
                firstEmptySeatPosition = customerPoints[i].position;
            }
        }
        if(!allSeatsReserved)
        {
            seatsUsed[seatReservedIndex] = true;
            customers[seatReservedIndex] = customer;
            customerNavMeshAgent.SetDestination(firstEmptySeatPosition);
        }
    }

    public void CustomerArrived(Customer customer)
    {
        for (int i = 0; i < numberOfSeats; i++)
        {
            if(customer == customers[i])
            {
                customersArrived[i] = true;
                CheckIfAllCustomersArrived();
            }
        }
    }

    void CheckIfAllCustomersArrived()
    {
        int numberOfCustomersArrived = 0;
        for (int i = 0; i < numberOfSeats; i++)
        {
            if(customersArrived[i] == true)
            {
                numberOfCustomersArrived++;
            }
        }
        if(numberOfCustomersArrived == numberOfSeats)
        {
            GenerateOrder();
        }
    }

    // Generates random order
    public void GenerateOrder()
    {
        // Currently only ClassicBurger is available
        nameOfCurrentOrder = "ClassicBurger";
        UpdateOrderImage();
    }

    public void UpdateOrderImage()
    {
        if(nameOfCurrentOrder == "none" || forceHideOrderSlot)
        {
            orderSlot.SetActive(false);
        }
        else
        {
            orderSlot.SetActive(true);
            if(nameOfCurrentOrder == "ClassicBurger")
            {
                orderImage.texture = orderTextures[0];
            }
        }
    }

    public void ResetOrderInfo()
    {
        forceHideOrderSlot = true;
        UpdateOrderImage();
    }

    public void ReceiveFood()
    {
        ResetOrderInfo();
        StartCoroutine(EatingProcess());
    }

    public IEnumerator EatingProcess()
    {
        SetClockFillVisibility(true);
        plateModel.SetActive(true);
        if (nameOfCurrentOrder == "ClassicBurger")
        {
            models[0].SetActive(true);
        }
        int progress = 0; // Max progress = 100
        UpdateTableClockFillValue(progress);
        while(progress < 100)
        {
            yield return new WaitForSecondsRealtime(.2f);
            progress++;
            UpdateTableClockFillValue(((float) progress) / 100);
        }
        SetClockFillVisibility(false);
        plateModel.SetActive(false);
        tableManagementScript.cashRegister.JoinQueue(this);
    }

    public void UpdateTableClockFillValue(float value)
    {
        tableClockFill.fillAmount = value;
    }

    // Sents first customer to cash register
    public void SentCustomerToPay()
    {
        tableManagementScript.cashRegister.CustomerGoingToRegister();
        customers[0].agent.SetDestination(tableManagementScript.cashRegister.customerPoint.position);
    }

    public void MakeAllCustomersLeave()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].agent.SetDestination(tableManagementScript.leavePoint.position);
            customers[i].leaving = true;
        }
        StartCoroutine(tableManagementScript.CustomersDelay());
        ResetEverything();
    }

    public void ResetEverything()
    {
        seatsUsed = new bool[numberOfSeats];
        customersArrived = new bool[numberOfSeats];
        customers = new Customer[numberOfSeats];
        for (int i = 0; i < numberOfSeats; i++)
        {
            seatsUsed[i] = false;
        }
        forceHideOrderSlot = false;
        nameOfCurrentOrder = "none";
        UpdateOrderImage();
        SetClockFillVisibility(false);
    }

    public bool AllSeatsReserved()
    {
        int numberOfReservedSeats = 0;
        for (int i = 0; i < numberOfSeats; i++)
        {
            if(seatsUsed[i])
            {
                numberOfReservedSeats++;
            }
        }
        if(numberOfReservedSeats == numberOfSeats)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
