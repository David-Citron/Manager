using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public GameObject deliveryManModel;
    public Transform spawnPoint;

    public Transform[] destinations;
    public Rack rack;
    public MoneyManager moneyManager;
    public int price = 100;

    public bool deliveryManOnMap = false;

    // Start is called before the first frame update
    void Start()
    {
        deliveryManOnMap = false;
        moneyManager = Camera.main.GetComponent<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDeliveryMan()
    {
        if(moneyManager.CurrentMoney() < price || deliveryManOnMap)
        {
            return;
        }
        moneyManager.RemoveMoney(price);
        GameObject deliveryMan = Instantiate(deliveryManModel, spawnPoint.position, spawnPoint.rotation);
        deliveryManOnMap = true;
        moneyManager.SupplyBoxOrderButtonColorUpdate();
    }
    
    public void DeliveryManLeaving()
    {
        deliveryManOnMap = false;
        moneyManager.SupplyBoxOrderButtonColorUpdate();
    }
}
