using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInventory 
{



    private game_master gm;
    private bool displayed = false;
    private GameObject invUI;


    public Inventory items;



    public void Start()
    {
        items = new Inventory();
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();

    }
    public void ToggleInventory()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();


        if (!displayed)
        {
            displayed = true;
            gm.DisplayInventory(items.items);
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
            gm.DisplayInventory(items.items);
        }
        State_satisfier ss = GameObject.FindObjectOfType<State_satisfier>(); ;
        ss.PossessiveStates();

    }


    public void AddItem(Item i)
    {
        items.AddItem(i);
        Refresh();
    }

    public void DelItem(Item i)
    {
        items.DelItem(i);
        Refresh();
    }

}


