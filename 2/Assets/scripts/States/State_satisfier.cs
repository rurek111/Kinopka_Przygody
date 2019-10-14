using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class State_satisfier : MonoBehaviour {
    public List <States> allStates;


    private GameObject p;
    private Player player;
    private Journal journal;



    // Use this for initialization
    void Start () {
        States [] temp  = FindObjectsOfType<States>();
        allStates = temp.ToList();


        p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();
        journal = player.journal;

    }


    // Update is called once per frame
    void Update ()
    {
        StateChangesQuest("QuinceStates", "has angry lyre", "The Feast of Sunatas", "Give the Lyre to Refisul", journal, true);//



    }

    public void PossessiveState(string statesName, string stateName, string itemName, PlayerInventory pI)
    {

        Item item = null;
        item = pI.items.items.Find(x => x.itemName == itemName);

        if (item != null)//
        {
            Satisfy(statesName, stateName);
        }
        else
        {
            Set(statesName, stateName, false);
        }
        
    }

    public void PossessiveStates()//fires when inventory is refreshed
    {

     PlayerInventory yourInv;

        yourInv = player.inventory;




        PossessiveState("QuinceStates", "has angry lyre", "Angry Lyre", yourInv);
        PossessiveState("QuinceStates", "has cup of water", "Cup of water", yourInv);

    }



    public void QuestChangesState(string statesName, string stateName, string questLineName, string questName, Journal j)
    {

        QuestLine questLine= null;
        questLine = j.undone.Find(x => x.questLineName == questLineName);
        if (questLine != null)//
        {

            if(questLine.ongoing!=null)
            {
                if (questLine.ongoing.toDo == questName)
                {
                    Satisfy(statesName, stateName);

                }
                else
                {
                    Set(statesName, stateName, false);

                }
            }
            else
            {
                Set(statesName, stateName, false);

            }

        }
        else
        {
            Set(statesName, stateName, false);
        }

    }


    public void StateChangesQuest(string statesName, string stateName, string questLineName, string toQuest, Journal j, bool toBe)
    {


        if(Compare( statesName,  stateName, toBe))
        {


            QuestLine questLine = null;
            questLine = j.undone.Find(x => x.questLineName == questLineName);

          
            if (questLine != null)//
            {
                Quest quest = null;


                if (questLine.ongoing != null)
                {
                    quest = questLine.ongoing.next.Find(x => x.toDo == toQuest);

                }

                questLine.Proceed(null, quest);

            }

        }


    }

    public void QuestStates()//fires when journal is refreshed
    {


        QuestChangesState("QuinceStates", "looks for Septina", "The Lost Fairy", "Gather some information on Septina", journal);//which state changes because of a quest
    }


    public void Satisfy(string statesName, string stateName)
    {
        States s = allStates.Find(i => i.statesName == statesName);
        s.Satisfy(stateName);
    }

    public void Set(string statesName, string stateName, bool toWhat)
    {
        States s = allStates.Find( i => i.statesName == statesName);
        if(s!=null)
        {
            if (toWhat)
            {
                s.Satisfy(stateName);

            }
            else
            {
                s.Dissatisfy(stateName);

            }
        }
       
    }

    public bool Compare(string statesName, string stateName, bool requirement)
    {
        States s = allStates.Find(i => i.statesName == statesName);
        return s.Compare( stateName,  requirement);

    }
}


//satisfaction by transfer, possession, simplyVlue(Type,name, where, value), sentence, existance, visibility
