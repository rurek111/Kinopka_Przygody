using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue_manager : MonoBehaviour {

    public Text nameBracket;
    public Text dialogueBracket;
    public Button[] buttons = new Button[4];
    Dialogues givenDialogues;
    Dialogue currentDialogue;
    public bool question = false;
    Animator anim;
    public bool talking = false;
    public bool terminate = false;


    private Queue <Sentence> sentences;
	// Use this for initialization

	public void Start () {
        sentences = new Queue<Sentence> ();


        buttons[0] = GameObject.Find("continue").GetComponent(typeof(Button)) as Button;
        buttons[1] = GameObject.Find("continue (1)").GetComponent(typeof(Button)) as Button;
        buttons[2] = GameObject.Find("continue (2)").GetComponent(typeof(Button)) as Button;
        buttons[3] = GameObject.Find("continue (3)").GetComponent(typeof(Button)) as Button;
        DisableButtons();

        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        nameBracket = GameObject.Find("speakers name").GetComponent(typeof(Text)) as Text;
    }
    void Awake()
    {
        buttons[0] = GetComponent<Button>();
        buttons[1] = GetComponent<Button>();
        buttons[2] = GetComponent<Button>();
        buttons[3] = GetComponent<Button>();

    }

    public void StartDialogue(Dialogues dialogues)
    {
        talking = true;
        nameBracket.text = dialogues.name;
        sentences.Clear();

        givenDialogues = dialogues;
        currentDialogue = null; //dialogues.dialogue[0];//
        foreach(Dialogue d in dialogues.dialogue)
        {
            if((d.prerequisiteForThisToBeBeggining.name.Length > 0)|| (d.prerequisiteForThisToBeBeggining.name.Length > 0))
            {
                if(d.prerequisiteForThisToBeBeggining.SatifiedPrerequisite())
                {
                    currentDialogue = d;//finds first fitting opening
                    break;
                }
            }
        }
        if(currentDialogue==null)
        {
            currentDialogue = dialogues.dialogue.Find(i => i.usualBeggining == true);//if didnt find
        }

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
        buttons[0].gameObject.SetActive(true);
        buttons[0].GetComponentInChildren<Text>().text = "Continue";
    }

    public void ShowContinuation(Dialogue dialogue, int i)
    {
        if((dialogue.continuations[i].prerequisite.name.Length<1)||(dialogue.continuations[i].prerequisite.statesName.Length < 1))
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].GetComponentInChildren<Text>().text = dialogue.continuations[i].buttonName;
        }
        else
        {
            if (dialogue.continuations[i].prerequisite.SatifiedPrerequisite() == true)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponentInChildren<Text>().text = dialogue.continuations[i].buttonName;
            }
        }
       
    }

    public void ShowButtons(Dialogue dialogue)
    {
        DisableButtons();
        ShowContinue();
        int answers = dialogue.continuations.Length;
        if (answers > 0)
        {
            ShowContinuation(dialogue, 0);

            if (answers > 1)
            {
                ShowContinuation(dialogue, 1);

                if (answers > 2)
                {
                    ShowContinuation(dialogue, 2);

                    if (answers > 3)
                    {
                        ShowContinuation(dialogue, 3);
                    }
                }
            }
        }
    }

    public void DisableButtons()
    {
        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
        buttons[2].gameObject.SetActive(false);
        buttons[3].gameObject.SetActive(false);
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
        Sentence sentence = new Sentence();
          sentence  = sentences.Dequeue();
        dialogueBracket.text = sentence.text;


        ExecuteExchanges(sentence);
        UpdateJournal(sentence);
        SatisfyStates(sentence);

    }

    public void EndDialogue()
    {
        nameBracket.text = ("");
        dialogueBracket.text = ("");

        DisableButtons();
        talking = false;
        terminate = true;

    }

    public void SatisfyStates(Sentence sentence)
    {
        if (sentence.changes.Length > 0)
        {
            foreach (StateToBeChanged t in sentence.changes)
            {
                if (t != null)
                {
                    State_satisfier stateSat = GameObject.FindGameObjectWithTag("game_master").GetComponent<State_satisfier>();
                    stateSat.Set(t.statesName, t.name, t.toBe);
                }
            }
        }
    }

        public void UpdateJournal(Sentence sentence)
    {

        if (sentence.questLineToProgress != null)
        {

            Player player = FindObjectOfType<Player>();

            if (player.journal.done.Contains(sentence.questLineToProgress))
            {
                return;
            }
            else
            {
                if (player.journal.undone.Contains(sentence.questLineToProgress))
                {
                    //
                    player.journal.undone.Find(x => x.Equals(sentence.questLineToProgress)).Proceed(sentence.fromQuest, sentence.toQuest);
                }
                else
                {
                    player.journal.StartQuestLine(sentence.questLineToProgress, sentence.toQuest);
                }
            }
               
        }
    }

    public void ExecuteExchanges(Sentence sentence)
    {
        if (sentence.exchanges.Length > 0)
        {
            foreach (Transfer t in sentence.exchanges)
            {
                if (t != null)
                {
                    t.Execute();
                }
            }
        }

    }
}


