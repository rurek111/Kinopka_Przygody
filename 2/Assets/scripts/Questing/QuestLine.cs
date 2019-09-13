using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New QuestLine", menuName = "QuestLine")]

public class QuestLine : ScriptableObject
{
    public bool ended = false, success = false;
    public string questLineName;
    public Reward reward;
    public Quest first;//starts with one order
    public List<Quest> last;//can end in multiple ways
    public Quest ongoing;


    public void Proceed(QuestLine questLine, Quest quest = null) //int path = 0 
    {
        ongoing.ended = true;
        ongoing.success = true;
        ongoing.reward.GiveReward();

        if(last.Contains(questLine.ongoing))
        {
            Debug.Log("QuestLine completed");
            ended = true;
            success = true;
            reward.GiveReward();

            //    Journal journal = FindObjectOfType<Journal>();
            Player player = FindObjectOfType<Player>();
            player.journal.FinishQuestLine(questLine);


            return;


        }
        else
        {
            if (quest == null)
            {
                ongoing = ongoing.next[0];
            }
            ongoing = quest;
        }

    }
}



