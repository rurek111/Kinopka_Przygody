using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCDialogueTrigger : MonoBehaviour {
    ///private Text nameBracket ;
    private Text dialogueBracket;
    private DialogueFlow flow;
    private Animator a;
	private bool inRange = false;
	private dialogue_manager dialMan;

    void Start()
    {
        a = gameObject.GetComponent(typeof(Animator)) as Animator;
        flow = gameObject.GetComponent(typeof(DialogueFlow)) as DialogueFlow;
        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        //nameBracket = GameObject.Find("speakers name").GetComponent(typeof(Text)) as Text;
		dialMan = FindObjectOfType<dialogue_manager>();
    }

	void Update()
	{
		if(dialMan.terminate == true)
		{
			StopSpeech();
		}


		if (inRange) 
		{			
			if (Input.GetKeyDown("e"))
			{
				if( dialMan.talking==false )
				{
					TriggerDialogue();



				}
			}
		}




	}

    public void TriggerDialogue()
    {
		a.SetBool("talking", true);
		dialMan.StartDialogue(flow.dialogues);
    }

    public void StopSpeech()
    {

        a.SetBool("talking", false);
		dialMan.terminate = false;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			inRange = true;

			if (dialMan.talking == false)///cause it fired when talking and player got closer to th verge of the hitbox, which made npcs say [E] to talk
			{
				dialogueBracket.text = ("[E] to talk"); 
			}
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

			inRange = true;
          //  dialogueBracket.text = ("[E] to talk");

		
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
		inRange = false;

        if (col.CompareTag("Player"))
        {
			dialMan.EndDialogue();
            StopSpeech();
        }
    }




    }
