using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue_manager : MonoBehaviour {

    public Text nameBracket;
    public Text dialogueBracket;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    Dialogues givenDialogues;
    Dialogue currentDialogue;
    public bool question = false;
    Animator anim;
    public bool finished = false;
    

    private Queue <Sentence> sentences;
	// Use this for initialization

	public void Start () {
        sentences = new Queue<Sentence> ();


        button1 = GameObject.Find("continue").GetComponent(typeof(Button)) as Button;
        button2 = GameObject.Find("continue (1)").GetComponent(typeof(Button)) as Button;
        button3 = GameObject.Find("continue (2)").GetComponent(typeof(Button)) as Button;
        button4 = GameObject.Find("continue (3)").GetComponent(typeof(Button)) as Button;
        DisableButtons();

        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        nameBracket = GameObject.Find("speakers name").GetComponent(typeof(Text)) as Text;
    }
    void Awake()
    {
        button1 = GetComponent<Button>();
        button2 = GetComponent<Button>();
        button3 = GetComponent<Button>();
        button4 = GetComponent<Button>();

    }

    public void StartDialogue(Dialogues dialogues)
    {
        finished = false;
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
        button1.gameObject.SetActive(true);
        button1.GetComponentInChildren<Text>().text = "Continue";
    }

    public void ShowButtons(Dialogue dialogue)
    {
        DisableButtons();
        ShowContinue();
        int answers = dialogue.continuations.Length;
        if (answers > 0)
        {
            
            //if(dialogue.continuations[0].prerequisites[].satisfied == true)
            button1.gameObject.SetActive(true);
            button1.GetComponentInChildren<Text>().text = dialogue.continuations[0].buttonName;

            if (answers > 1)
            {
                button2.gameObject.SetActive(true);
                button2.GetComponentInChildren<Text>().text = dialogue.continuations[1].buttonName;
                if (answers > 2)
                {
                    button3.gameObject.SetActive(true);
                    button3.GetComponentInChildren<Text>().text = dialogue.continuations[2].buttonName;
                    if (answers > 3)
                    {
                        button4.gameObject.SetActive(true);
                        button4.GetComponentInChildren<Text>().text = dialogue.continuations[3].buttonName;

                    }
                }
            }
        }
    }

    public void DisableButtons()
    {
       
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
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
            currentDialogue = dialogueToAdd;
            AddMore(dialogueToAdd);
            question = false;
            DisplayNextSentance();

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
        if(sentence.exchanges.Length!=0)
        {
            foreach(Transfer t in sentence.exchanges)
            {
                t.Execute();
            }
        }

    }

    public void EndDialogue()
    {
        nameBracket.text = ("");
        dialogueBracket.text = ("");

        DisableButtons();
        finished = true;

    }
}


