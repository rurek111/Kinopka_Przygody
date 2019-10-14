using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal:MonoBehaviour
{

    public List<QuestLine> undone, done;
    public bool displayed = false;
    game_master gm;

    public void Start()
    {
       // done = new List<QuestLine>;
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();
    }

    public void ToggleJournal()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();


        if (!displayed)
        {
            displayed = true;
            gm.DisplayJournal(done, undone);
        }
        else
        {
            displayed = false;
            gm.HideJournal();
        }
    }

    public void Refresh()
    {
        gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();

        gm.HideJournal();

        if (displayed)
        {
            gm.DisplayJournal(done, undone);
        }

        State_satisfier ss = GameObject.FindObjectOfType<State_satisfier>(); ;
        ss.QuestStates();
    }

    public void StartQuestLine(QuestLine questLine, Quest quest = null)
    {
        if(quest == null)
        {
            quest = questLine.first;
        }
        questLine.ongoing = quest;
        undone.Add(questLine);
        Refresh();
    }

    public void FinishQuestLine(QuestLine questLine)
    {
        done.Add(questLine);
        undone.Remove(questLine);
        Refresh();

    }

}


