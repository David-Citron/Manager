using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    private int money = 0;
    public TMP_Text moneyText;
    public RawImage supplyBoxOrderButton;
    public Color32 affordableColor;
    public Color32 notAffordableColor;
    public OrderManager orderManager;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        orderManager = GetComponent<OrderManager>();
        UpdateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddMoney(int value)
    {
        money += value;
        UpdateMoneyText();
    }

    public int CurrentMoney()
    {
        return money;
    }

    public void RemoveMoney(int value)
    {
        money -= value;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = money + "$";
        SupplyBoxOrderButtonColorUpdate();
    }

    public void SupplyBoxOrderButtonColorUpdate()
    {
        if(money >= orderManager.price && !orderManager.deliveryManOnMap)
        {
            supplyBoxOrderButton.color = affordableColor;
        }else
        {
            supplyBoxOrderButton.color = notAffordableColor;
        }
    }
}
