using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory 
{



    private game_master gm;
    private bool displayed = false;
    private GameObject invUI;


    public List<Item> items;



    public void Start()
    {
        items = new List<Item>();
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();

    }
    public void ToggleInventory()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();


        if (!displayed)
        {
            displayed = true;
            gm.DisplayInventory(items);
        }
        else
        {
            displayed = false;
            gm.HideInventory();
        }
    }

    public void Refresh()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();

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


