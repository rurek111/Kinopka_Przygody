using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCDialogueTrigger : MonoBehaviour {
    private Text nameBracket ;
    private Text dialogueBracket;
    private DialogueFlow flow;
    private Animator a;

    void Start()
    {
        a = gameObject.GetComponent(typeof(Animator)) as Animator;
        flow = gameObject.GetComponent(typeof(DialogueFlow)) as DialogueFlow;
        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        nameBracket = GameObject.Find("speakers name").GetComponent(typeof(Text)) as Text;

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogue_manager>().StartDialogue(flow.dialogues);
        
    }

    public void StopSpeach()
    {
        nameBracket.text = ("");
        dialogueBracket.text = ("");
        FindObjectOfType<dialogue_manager>().EndDialogue();
        a.SetBool("talking", false);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = ("[E] to talk");
            if ((Input.GetKeyDown("e")) && ( FindObjectOfType<dialogue_manager>().talking==false ))
            {
                TriggerDialogue();
                a.SetBool("talking", true);

            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if ((Input.GetKeyDown("e")) && (FindObjectOfType<dialogue_manager>().talking == false))
            {
                TriggerDialogue();
                a.SetBool("talking", true);

            }

            if(FindObjectOfType<dialogue_manager>().talking == false)
            {
                StopSpeach();

            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StopSpeach();
        }
    }




    }
