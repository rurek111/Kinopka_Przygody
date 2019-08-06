﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCDialogueTrigger : MonoBehaviour {
    public Text nameBracket;
    public Text dialogueBracket;
  ///  private game_master gm;
    public DialogueFlow flow;

    void Start()
    {
     //   gm = GameObject.FindGameObjectWithTag("game_master").GetComponent<game_master>();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<dialogue_manager>().StartDialogue(flow.dialogues);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = ("[E] to talk");
            if (Input.GetKey("e"))
            {
                TriggerDialogue();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
            {
                TriggerDialogue();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            nameBracket.text = (" ");
            dialogueBracket.text = (" ");
            FindObjectOfType<dialogue_manager>().EndDialogue();
        }
    }




    }
