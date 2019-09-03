using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New QuestLine", menuName = "QuestLine")]

public class QuestLine : ScriptableObject
{
    public bool ended, success;
    public string questLineName;
    public Reward reward;
    public Quest first;//starts with one order
    public List<Quest> last;//can end in multiple ways
    public Quest ongoing;
}


public class Journal
{
    public List<QuestLine> undone, done;

}