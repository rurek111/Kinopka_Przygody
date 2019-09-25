using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogues
{
    public string name;
    public Dialogue[] dialogue;
}

[System.Serializable]
public class Dialogue
{
    public string dialogueName;
    public Sentence[] sentences;
    public Continuations[] continuations;

}

[System.Serializable]
public class Continuations
{
    public int nextDialogueIndex;
    public string buttonName;
//   public Prerequisite[] prerequisites;
}




[System.Serializable]

public class Sentence
{

    [TextArea(3, 15)]

    public string text;
    public QuestLine questLineToProgress;
    public Quest questHowToProgress;
    public Transfer[] exchanges;
    public StateToBeChanged[] changes;

}