using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{ 
    string nameOfOrder = "none";

    // Start is called before the first frame update
    void Start()
    {
        
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
        }else if(randomNumber == 1)
        {
            nameOfOrder = "Double Burger";
        }
    }
}
