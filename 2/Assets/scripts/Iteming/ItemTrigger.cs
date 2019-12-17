using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTrigger : MonoBehaviour {

    private Text dialogueBracket;
    public Item item;
	private bool inRange = false;


    void Start()
    {
        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
    }

	void Update()
	{
		if (inRange) 
		{
			if (Input.GetKeyDown ("e")) 
			{
				PickUp ();
			}
		}
	}

    void PickUp()
    {
       // Debug.Log("Picked item");

        FindObjectOfType<Player>().inventory.AddItem(item);
        if (dialogueBracket.text == ("[E] to take"))
        {
            dialogueBracket.text = (" ");
        }
        GameObject.Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			inRange = true;
            dialogueBracket.text = ("[E] to take");
           
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			inRange = true;
          
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			inRange = false;
            if(dialogueBracket.text == ("[E] to take"))
            {
                dialogueBracket.text = (" ");
            }
           // if (Input.GetKeyDown("e")){ PickUp();}
        }
    }

}
