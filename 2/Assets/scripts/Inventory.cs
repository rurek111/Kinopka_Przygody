using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory
{
    public Text textBracket;
    public List<Item> items;
    public bool displayed = false;

    void Start()
    {
        items = new List<Item>();
    }

    void Update()
    {
       
    }

    public void ToggleInventory()
    {
        if( !displayed)
        {
            DisplayInventory();
            displayed = true;
        }
        else
        {
            textBracket.text = (" ");
            displayed = false;
        }
    }

    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void DelItem(Item i)
    {
        items.Remove(i);
    }

    public void DisplayInventory()
    {
        foreach(Item i in items)
        {
            textBracket.text += i.name + "\n";
        }

    }

}
