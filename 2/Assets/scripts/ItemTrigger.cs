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
                dialogueBracket.text = (" ");
                GameObject.Destroy(gameObject);
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
                dialogueBracket.text = (" ");
                GameObject.Destroy(gameObject);

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
