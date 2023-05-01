using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{ 
    public Table assignedTable;
    public NavMeshAgent agent;
    public bool seated;
    public bool leaving;

    // Start is called before the first frame update
    void Start()
    {
        seated = false;
        assignedTable = Camera.main.GetComponent<TableManagementScript>().AssignFreeTable();
        agent = GetComponent<NavMeshAgent>();
        leaving = false;
        assignedTable.CustomerSeatRequest(agent, this);
    }

    IEnumerator Starting()
    {
        yield return new WaitForEndOfFrame();
        assignedTable.CustomerSeatRequest(agent, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CustomerSeat")
        {
            assignedTable.CustomerArrived(this);
        }
        else if(other.tag == "CustomerCashRegister")
        {
            assignedTable.tableManagementScript.cashRegister.CustomerArriving();
        }
        
        // Add other conditions here:

        else if(other.tag == "CustomerLeavingPoint")
        {
            if(leaving)
            {
                Destroy(this.gameObject);
            }
        }
    }

    
}
