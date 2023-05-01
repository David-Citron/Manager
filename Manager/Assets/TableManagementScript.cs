using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManagementScript : MonoBehaviour
{
    public Texture[] orderTextures;
    public Transform leavePoint;

    public Table[] tables;
    public CashRegister cashRegister;

    public GameObject customerModel;
    public Transform spawnPoint;
    public List<Customer> customers = new List<Customer>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstSpawning());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnCustomer()
    {
        Table currentTable = AssignFreeTable();
        int numberOfCustomersRequired = currentTable.numberOfSeats;
        while (numberOfCustomersRequired > 0)
        {
            numberOfCustomersRequired--;
            GameObject newCustomer = Instantiate(customerModel, spawnPoint.position, spawnPoint.rotation);
            Customer currentCustomer = newCustomer.GetComponent<Customer>();
            customers.Add(currentCustomer);
            currentCustomer.assignedTable = currentTable;
            yield return new WaitForSecondsRealtime(1.5f);
        }
        
    }

    public bool IsAnyTableFree()
    {
        bool isFree = false;
        for (int i = 0; i < tables.Length; i++)
        {
            if (!tables[i].AllSeatsReserved())
            {
                isFree = true;
            }
        }
        return isFree;
    }

    public Table AssignFreeTable()
    {
        Table freeTable = null;
        for (int i = 0; i < tables.Length; i++)
        {
            if(!tables[i].AllSeatsReserved() && freeTable == null)
            {
                freeTable = tables[i];
            }
        }
        return freeTable;
    }

    public IEnumerator CustomersDelay()
    {
        yield return new WaitForSecondsRealtime(10);
        StartCoroutine(SpawnCustomer());
    }

    public IEnumerator FirstSpawning()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < tables.Length; i++)
        {
            StartCoroutine(SpawnCustomer());
            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
