using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Reward", menuName = "Reward")]
public class Reward : ScriptableObject
{
    public int exp;
    public List<Item> items;
}

