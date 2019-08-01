using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue_manager : MonoBehaviour {

    public Text nameBracket;
    public Text dialogueBracket;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    Dialogues givenDialogues;
    Dialogue currentDialogue;
    public bool question = false;

    private Queue <Sentence> sentences;
	// Use this for initialization

	void Start () {
        sentences = new Queue<Sentence> ();

    }


    public void StartDialogue(Dialogues dialogues)
    {
        nameBracket.text = dialogues.name;
        sentences.Clear();

        givenDialogues = dialogues;
        currentDialogue = dialogues.dialogue[0];

        AddMore(currentDialogue);

        DisplayNextSentance();
    }

    public void AddMore(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        currentDialogue = dialogue;
    }
	
    public void ShowContinue()
    {
        DisableButtons();
        button1.SetActive(true);
        button1.GetComponentInChildren<Text>().text = "Continue";
    }

    public void ShowButtons(Dialogue dialogue)
    {
        DisableButtons();
        ShowContinue();
        int answers = dialogue.continuations.Length;
        if (answers > 0)
        {
            button1.SetActive(true);
            button1.GetComponentInChildren<Text>().text = dialogue.continuations[0].buttonName;
            if (answers > 1)
            {
                button2.SetActive(true);
                button2.GetComponentInChildren<Text>().text = dialogue.continuations[1].buttonName;
                if (answers > 2)
                {
                    button3.SetActive(true);
                    button3.GetComponentInChildren<Text>().text = dialogue.continuations[2].buttonName;
                    if (answers > 3)
                    {
                        button4.SetActive(true);
                        button4.GetComponentInChildren<Text>().text = dialogue.continuations[3].buttonName;

                    }
                }
            }
        }
    }

    public void DisableButtons()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
    }

    public void ChoiceMade( int j)
    {
        
        if(question == false)
        {
            DisableButtons();

            DisplayNextSentance();

        }
        else if (j < 0 || j >= currentDialogue.continuations.Length)
        {
            EndDialogue();
        }
        else if(question == true)
        {
            DisableButtons();
            int dialogueIndex = currentDialogue.continuations[j].nextDialogueIndex;
            if(dialogueIndex<0)
            {
                EndDialogue();
                return;
            }
            Dialogue dialogueToAdd = givenDialogues.dialogue[dialogueIndex];
            AddMore(dialogueToAdd);
            DisplayNextSentance();
            question = false;
        }

    }

    public void DisplayNextSentance()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else if (currentDialogue.sentences.Length == 1 || sentences.Count == 1)
        {
            ShowButtons(currentDialogue);
            question = true;
        }
        else
        {
            ShowContinue();
            question = false;
        }
        Sentence sentence = sentences.Dequeue();
        dialogueBracket.text = sentence.text;
    }

    public void EndDialogue()
    {
        nameBracket.text = (" ");
        dialogueBracket.text = (" ");

        DisableButtons();


    }
}


