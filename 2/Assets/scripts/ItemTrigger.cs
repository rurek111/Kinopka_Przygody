using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemTrigger : MonoBehaviour {

    public Text dialogueBracket;
    public Item item;

    void PickUp()
    {
        FindObjectOfType<player>().inventory.AddItem(item);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogueBracket.text = ("[E] to take");
            if (Input.GetKey("e"))
            {
                PickUp();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey("e"))
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
