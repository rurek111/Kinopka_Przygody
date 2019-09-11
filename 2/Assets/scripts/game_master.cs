using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_master : MonoBehaviour
{

	public int points;
	public Text pointsText;
	public Text input_text;
   // public Text inv_text;
    public GameObject invUI;
    private GameObject item1;
    private Vector2 delta_img;
    private List <GameObject> inv;

    void Start()
    {
        input_text = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
       // inv_text = GameObject.Find("inventory text").GetComponent(typeof(Text)) as Text;

        pointsText = GameObject.Find("burak_counter").GetComponent(typeof(Text)) as Text;

        invUI = GameObject.Find("Inventory UI");
        item1 = GameObject.Find("item");
        delta_img = item1.GetComponentInChildren<RectTransform>().sizeDelta;


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
   //     inv_text.text = ("");
        ErasePictures();

        invUI.SetActive(false);
    }

    public void DisplayInventory(List<Item> items)
    {
        invUI.SetActive(true);

     //   foreach (Item i in items)
    //    {
     //       inv_text.text += i.itemName + "\n";
     //   }

        InvPictures(items);

    }


    public void InvPictures(List<Item> items, int space_width = 1)
    {
        int n = 0;


        inv = new List<GameObject>();

        foreach (Item i in items)
        {
            inv.Add(Instantiate(item1, item1.transform.parent));


            Vector3 newPosition = item1.transform.position;
            newPosition.y = newPosition.y - space_width * n;
            inv[n].transform.position = newPosition;


            //         r.sprite.bounds.SetMinMax(new Vector3(-0.1f, -0.1f, 0.0f), new Vector3(0.1f, 0.1f, 0.0f));

 
            RectTransform ImgRT = inv[n].GetComponentInChildren<RectTransform>();
      //      ImgRT.sizeDelta = new Vector2(2f, 2f);


            SpriteRenderer r = inv[n].GetComponentInChildren<SpriteRenderer>();
            r.sprite = i.s;

            ImgRT.sizeDelta = delta_img;
            ImgRT.localScale -= new Vector3(1F, 1F, 0);



            Text t = inv[n].GetComponent<Text>();
            t.text = i.itemName;

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




