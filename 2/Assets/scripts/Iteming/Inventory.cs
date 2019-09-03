using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public List<Item> items;



    public void Start()
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
