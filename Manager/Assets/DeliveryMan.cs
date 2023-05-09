using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryMan : MonoBehaviour
{
    Transform leavePoint;
    NavMeshAgent agent;
    bool arrived = false;
    public Rack rack;
    public int suppliesCarrying;
    public OrderManager orderManager;

    // Start is called before the first frame update
    void Start()
    {
        suppliesCarrying = 4;
        orderManager = Camera.main.GetComponent<OrderManager>();
        rack = orderManager.rack;
        agent = GetComponent<NavMeshAgent>();
        leavePoint = Camera.main.GetComponent<TableManagementScript>().leavePoint;
        GoTo(orderManager.destinations[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rack")
        {
            arrived = true;
            StartCoroutine(StockingUp());
        }else if(other.tag == "CustomerLeavingPoint" && arrived)
        {
            orderManager.DeliveryManLeaving();
            Destroy(this.gameObject);
        }
    }

    public IEnumerator StockingUp()
    {
        yield return new WaitForSecondsRealtime(5);
        rack.AddSupplies(4);
        ReturnBack();
    }

    public void ReturnBack()
    {
        agent.SetDestination(leavePoint.position);
    }

    public void GoTo(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
