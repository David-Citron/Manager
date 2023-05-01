using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject interactionMenu;
    public RawImage inventoryImage;

    public CashRegister cashRegister;
    public Grill grill;
    public Rack rack;
    public Fridge fridge;
    public Plate plate;
    public Table table1;
    public Table table2;

    // Name of an item that is currently in inventory
    [SerializeField] string itemInInventory = "none";
    // itemInInventory - Item names:
    // 0. none
    // 1. RawMeat
    // 2. CookedMeat
    // 3. BurnedMeat
    // 4. SupplyCrate
    // 5. ClassicBurger

    // Represents in which collider is player currently in
    [SerializeField] string interactionRange = "none";
    // interactionRange - Collider names:
    // 0. none
    // 1. CashRegister
    // 2. Grill
    // 3. Rack
    // 4. Fridge
    // 5. Table 1
    // 6. Plate

    // Images for each item in inventory with indexes
    [SerializeField] Texture[] itemImages = new Texture[1];
    // itemImages - items and their image index:
    // 0. RawMeat
    // 1. CookedMeat
    // 2. BurnedMeat
    // 3. SupplyCrate
    // 4. ClassicBurger
   

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        ShowInteractionMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "CashRegister")
        {
            interactionRange = "CashRegister";
        }
        else if(other.transform.tag == "Grill")
        {
            interactionRange = "Grill";
        }
        else if(other.transform.tag == "Rack")
        {
            interactionRange = "Rack";
        }
        else if(other.transform.tag == "Fridge")
        {
            interactionRange = "Fridge";
        }
        else if (other.transform.tag == "Table 1")
        {
            interactionRange = "Table 1";
        }
        else if (other.transform.tag == "Table 2")
        {
            interactionRange = "Table 2";
        }
        else if (other.transform.tag == "Plate")
        {
            interactionRange = "Plate";
        }

        if (interactionRange != "none")
        {
            ShowInteractionMenu(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionRange = "none";
        ShowInteractionMenu(false);
    }

    public void GoToPosition(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    private void ShowInteractionMenu(bool value)
    {
        interactionMenu.SetActive(value);
    }

    // Pressed Interaction button or Space
    public void Interaction()
    {
        if(interactionRange == "Fridge")
        {
            AddItemToInventory("RawMeat");
        }
        else if(interactionRange == "Grill")
        {
            grill.PlayerInteractionRequest(itemInInventory);
        }
        else if (interactionRange == "Rack")
        {
            // Taking crate from the rack
            if(rack.CanTakeSupplies() && itemInInventory == "none")
            {
                rack.TakeSupplies();
                AddItemToInventory("SupplyCrate");
            }

            // Restocking

        }
        else if (interactionRange == "Plate")
        {
            // Taking the plate
            if(itemInInventory == "none" && plate.IsPlateSetUp() && plate.CanCurrentlyTakeThePlate())
            {
                AddItemToInventory(plate.TakePlate());
            }
            // Adding item on the plate
            else if(itemInInventory != "none" && itemInInventory != "SupplyCrate" && plate.IsPlateSetUp() && plate.CanAddThisItemOnThePlate(itemInInventory))
            {
                plate.AddingItemOnThePlate(itemInInventory);
                RemoveItemFromInventory();
            }
            // Setting up the plate
            else if(itemInInventory == "SupplyCrate" && !plate.IsPlateSetUp())
            {
                plate.AddPlate();
                RemoveItemFromInventory();
            }
            // Putting plate back
            else if(itemInInventory == "ClassicBurger" && !plate.IsPlateSetUp())
            {
                plate.AddPlate();
                plate.AddingItemOnThePlate("CookedMeat");
                RemoveItemFromInventory();
            }
        }
        else if(interactionRange == "Table 1" && itemInInventory == table1.nameOfCurrentOrder)
        {
            table1.ReceiveFood();
            RemoveItemFromInventory();
        }
        else if (interactionRange == "Table 2" && itemInInventory == table2.nameOfCurrentOrder)
        {
            table2.ReceiveFood();
            RemoveItemFromInventory();
        }
        else if(interactionRange == "CashRegister" && cashRegister.IsCustomerAtTheCashRegister())
        {
            cashRegister.CustomerPayingAtTheCashRegister();
        }
    }

    public void AddItemToInventory(string itemName)
    {
        if(itemInInventory == "none")
        {
            itemInInventory = itemName;
            UpdateInventoryImage();
        }
    }

    public void UpdateInventoryImage()
    {
        if(itemInInventory == "none")
        {
            inventoryImage.gameObject.SetActive(false);
            return;
        }
        inventoryImage.gameObject.SetActive(true);
        if(itemInInventory == "RawMeat")
        {
            inventoryImage.texture = itemImages[0];
        }
        else if (itemInInventory == "CookedMeat")
        {
            inventoryImage.texture = itemImages[1];
        }
        else if (itemInInventory == "BurnedMeat")
        {
            inventoryImage.texture = itemImages[2];
        }
        else if (itemInInventory == "SupplyCrate")
        {
            inventoryImage.texture = itemImages[3];
        }
        else if (itemInInventory == "ClassicBurger")
        {
            inventoryImage.texture = itemImages[4];
        }
    }

    public void RemoveItemFromInventory()
    {
        itemInInventory = "none";
        UpdateInventoryImage();
    }
}