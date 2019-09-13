using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Reward", menuName = "Reward")]
public class Reward : ScriptableObject
{
    public int exp;
    public List<Item> items;


    public void GiveReward()
    {
        Player player = FindObjectOfType<Player>();
        foreach (Item i in items)
        {
            player.inventory.AddItem(i);
        }
        player.exp += exp;
    }
}



