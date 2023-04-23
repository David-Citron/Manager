using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Table : MonoBehaviour
{
    public RawImage tableButtonImage;
    public Color32 tableButtonHighlightColor;
    private Color32 tableButtonDefaultColor;
    public Player player;
    public Transform tablePositionPoint;
    public ActionButton tableButton;
    public string axisName = "Table 1";
    int numberOfSeats = 0;

    public Transform[] customerPoints;
    public bool[] seatsUsed;
    public Customer[] customers;

    // Start is called before the first frame update
    void Start()
    {
        tableButtonDefaultColor = tableButtonImage.color;
        numberOfSeats = customerPoints.Length;
        seatsUsed = new bool[numberOfSeats];
        customers = new Customer[numberOfSeats];
        for (int i = 0; i < numberOfSeats; i++)
        {
            seatsUsed[i] = false;
        }
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

    public void customerSeatRequest(NavMeshAgent customerNavMeshAgent, Customer customer)
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
            customerNavMeshAgent.SetDestination(firstEmptySeatPosition);
        }
    }
}
