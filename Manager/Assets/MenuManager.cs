using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] menus;
    public GameObject[] menuItems;
    public Color32 highlightedColor;
    Color32 defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = menuItems[0].GetComponent<RawImage>().color;
        ShowMenu(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(i == index)
            {
                menus[i].SetActive(true);
            }else
            {
                menus[i].SetActive(false);
            }
        }
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (i == index)
            {
                menuItems[i].GetComponent<RawImage>().color = highlightedColor;
            }
            else
            {
                menuItems[i].GetComponent<RawImage>().color = defaultColor;
            }
        }
    }
}
