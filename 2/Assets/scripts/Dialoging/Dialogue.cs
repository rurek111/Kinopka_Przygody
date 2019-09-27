using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;
    public List <Dialogue> dialogue;
}

[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    public Sentence[] sentences;
    public Continuations[] continuations;
    public StateToBeChanged prerequisiteForThisToBeBeggining = null;
    public bool usualBeggining = false;

}

[System.Serializable]
public class Continuations
{
    public int nextDialogueIndex;
    public string buttonName;
   public StateToBeChanged prerequisite;



}




[System.Serializable]

public class Sentence
{

    [TextArea(3, 15)]

    public string text;
    public QuestLine questLineToProgress;
    public Quest fromQuest, toQuest;
    public Transfer[] exchanges;
    public StateToBeChanged[] changes;

}