using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTrigger : MonoBehaviour {

    private Text dialogueBracket;
    public Item item;

    void Start()
    {
        dialogueBracket = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
    }

    void PickUp()
    {
        FindObjectOfType<Player>().inventory.AddItem(item);
        dialogueBracket.text = (" ");
        GameObject.Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = ("[E] to take");
            if (Input.GetKeyDown("e"))
            {
                PickUp();

            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown("e"))
            {
                PickUp();
 
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = (" ");
        }
    }

}
