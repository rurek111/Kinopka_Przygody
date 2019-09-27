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

    void Start()
    {
        a = gameObject.GetComponent(typeof(Animator)) as Animator;
        flow = gameObject.GetComponent(typeof(DialogueFlow)) as DialogueFlow;
        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        //nameBracket = GameObject.Find("speakers name").GetComponent(typeof(Text)) as Text;

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogue_manager>().StartDialogue(flow.dialogues);
        a.SetBool("talking", true);

    }

    public void StopSpeech()
    {

        a.SetBool("talking", false);
        FindObjectOfType<dialogue_manager>().terminate = false;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = ("[E] to talk");
            if ((Input.GetKeyDown("e")) && ( FindObjectOfType<dialogue_manager>().talking==false ))
            {
                TriggerDialogue();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
          //  dialogueBracket.text = ("[E] to talk");

            if ((Input.GetKeyDown("e")) && (FindObjectOfType<dialogue_manager>().talking == false))
            {
                TriggerDialogue();
            }

            if(FindObjectOfType<dialogue_manager>().terminate == true)
            {
                StopSpeech();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<dialogue_manager>().EndDialogue();
            StopSpeech();
        }
    }




    }
