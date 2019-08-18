using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory
{
   // public Text textBracket;
    public List<Item> items;
    private bool displayed = false;
    private game_master gm;

    private GameObject invUI;

    public void Start()
    {
        items = new List<Item>();
       
    }

    void Update()
    {
       
    }

    public void ToggleInventory()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();
        

        if ( !displayed)
        {
            gm.DisplayInventory(items);
            displayed = true;
        }
        else
        {
            displayed = false;
            gm.HideInventory();
        }
    }

    public void Refresh()
    {
        gm.HideInventory();

        if (displayed)
        {
            gm.DisplayInventory(items);
        }
     

    }

    public void AddItem(Item i)
    {
        items.Add(i);
        Refresh();
    }

    public void DelItem(Item i)
    {
        items.Remove(i);
        Refresh();
    }




   

}
