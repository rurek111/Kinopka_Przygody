using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{

    public List<Item> items;


    void Start()
    {
        items = new List<Item>();
    }

    void Update()
    {

    }


    public void AddItem(Item i)
    {
        items.Add(i);
    }

    public void DelItem(Item i)
    {
        items.Remove(i);
    }

}

[System.Serializable]
public class Item : MonoBehaviour {


    public string name;
    public int value;



}
