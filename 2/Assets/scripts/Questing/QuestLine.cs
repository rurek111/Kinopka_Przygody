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

    public void ProceedPass(Quest to = null)
    {

        if(ongoing != null)
        {
            ongoing.ended = true;
            ongoing.success = true;
            if(ongoing.reward !=null)
            {
                ongoing.reward.GiveReward();

            }


            if (last.Contains(ongoing))
            {
                Debug.Log("QuestLine completed");
                ended = true;
                success = true;
                if(reward!=null)
                {
                    reward.GiveReward();

                }

                //    Journal journal = FindObjectOfType<Journal>();
                Player player = FindObjectOfType<Player>();
                player.journal.FinishQuestLine(this);

                player.journal.Refresh();

                return;

            }

            else
            {
                if (to == null)
                {
                    ongoing = ongoing.next[0];
                }
                if (ongoing.next.Contains(to))
                    ongoing = to;

                Player player = FindObjectOfType<Player>();
                player.journal.Refresh();
            }
     
        }
        
      


    }
    public void Proceed(Quest from = null, Quest to = null) //int path = 0 
    {

        if(from == null && to == null)
        {
            return;
        }



      if(to!=null)
        {
            if (to == ongoing)
            {
                return;
            }

            if (to.ended)
            {
                return;
            }
        }
        else//probably first one firing again
        {
            if (to == first)
            {
                ongoing = to;//so just for sure
                return;
            }
        }


        if (from == null)
        {
            ProceedPass(to);
        }
        else
        {
            if (from == ongoing)
            {
                ProceedPass(to);
            }
            else
            {
                return;
            }
        }
       

        //should be done?
    }


    public  void GoBack(Quest from = null, Quest to = null)
    {

        if (from == null && to == null)
        {
            return;
        }

        if(to==null)
        {
            return;
        }
        if(ongoing == from)
        {
            ongoing.ended = false;
            ongoing = to;
            ongoing.ended = false;
            Player player = FindObjectOfType<Player>();
            player.journal.Refresh();
            return;
        }
        if(from == null)
        {
            ongoing.ended = false;
            ongoing = to;
            ongoing.ended = false;
            Player player = FindObjectOfType<Player>();
            player.journal.Refresh();
            return;
        }


    }


    }



