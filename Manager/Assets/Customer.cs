using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{ 
    string nameOfOrder = "none";
    public Table assignedTable;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        assignedTable = Camera.main.GetComponent<TableManagementScript>().AssignFreeTable();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        yield return new WaitForEndOfFrame();
        assignedTable.customerSeatRequest(agent, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateOrder()
    {
        int randomNumber = Random.Range(0, 2);
        if(randomNumber == 0)
        {
            nameOfOrder = "Classic Burger";
        }else
        {
            nameOfOrder = "Classic Burger";
        }
    }
}
