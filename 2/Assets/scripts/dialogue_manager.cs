using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dialogue_manager : MonoBehaviour {

    public Text nameBracket;
    public Text dialogueBracket;

    private Queue <string> sentences;
	// Use this for initialization

	void Start () {
        sentences = new Queue<string> ();
	}


    public void StartDialogue(Dialogue dialogue)
    {
        nameBracket.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentance();
    }
	
    public void DisplayNextSentance()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueBracket.text = sentence;
    }

    void EndDialogue()
    {
        nameBracket.text = (" ");
        dialogueBracket.text = (" ");

    }
}
