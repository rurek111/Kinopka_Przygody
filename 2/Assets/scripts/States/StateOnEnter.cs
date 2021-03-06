﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOnEnter : MonoBehaviour {
    public QuestLine questLine;
    public Quest from, to, alt = null;




    private GameObject p;
    private Player player;
    private Journal journal;





    // Use this for initialization
    void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();
        journal = player.journal;


       
    }

    // Update is called once per frame
    void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(journal.undone.Contains(questLine))
            {
                
                QuestLine q;
                q = journal.undone.Find(x => x = questLine);
                if (q.ongoing == from)
                {
                    q.Proceed(from, to);
                }
                else
                {
                    if (q.ongoing == alt)
                    {
                        q.Proceed(alt, to);
                    }
                }
             
            }

        }
    }



    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
    


        }
    }




    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (journal.undone.Contains(questLine))
            {
                QuestLine q;
                q = journal.undone.Find(x => x = questLine);

                if (q.ongoing == to)
                {
                    if(alt == null)
                    {
                        q.GoBack(to, from);
                    }
                    else
                    {
                        q.GoBack(to, alt);
                    }
                }
            }

        }
    }
}
