using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_master : MonoBehaviour
{

	public int points;
	public Text pointsText;
	public Text input_text;
    public Text inv_text;
    public GameObject invUI;
    private GameObject inv1;
    private List <GameObject> inv;

    void Start()
    {
        input_text = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        inv_text = GameObject.Find("inventory text").GetComponent(typeof(Text)) as Text;

        pointsText = GameObject.Find("burak_counter").GetComponent(typeof(Text)) as Text;

        invUI = GameObject.Find("Inventory UI");
        inv1 = GameObject.Find("inv_img");
        inv = new List<GameObject>();

        HideInventory();

    }

    void Update()
	{
	}

    public void Points()
    {
        pointsText.text = ("Points: " + points);

    }

    public void HideInventory()
    {
        inv_text.text = ("");
        ErasePictures();

        invUI.SetActive(false);
    }

    public void DisplayInventory(List<Item> items)
    {
        invUI.SetActive(true);

        foreach (Item i in items)
        {
            inv_text.text += i.name + "\n";
        }

        InvPictures(items);

    }


    public void InvPictures(List<Item> items)
    {
        int n = 0;


        inv = new List<GameObject>();

        foreach (Item i in items)
        {
            inv.Add(Instantiate(inv1, inv1.transform.parent));


            Vector3 newPosition = inv1.transform.position;
            newPosition.y = newPosition.y - 1;
            inv[n].transform.position = newPosition;


            SpriteRenderer r = inv[n].GetComponent<SpriteRenderer>();
            r.sprite = i.s;
            

            n++;
        }

    }

    public void ErasePictures()
    {

        if (inv.Count != 0)
        {

            foreach (GameObject i in inv)
            {
                if (i != null)
                {
                    Destroy(i);
                    inv = new List<GameObject>();
                }
            }

        }
    }
}




