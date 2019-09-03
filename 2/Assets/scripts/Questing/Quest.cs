using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public bool ended, success;
    public string toDo;
    public Reward reward;
    public List <Quest> next;//can have multiple successors

}



