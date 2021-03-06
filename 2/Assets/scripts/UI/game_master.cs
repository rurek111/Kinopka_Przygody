﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_master : MonoBehaviour //mostly UI
{

	public int points;
	public Text pointsText;
	public Text input_text;
    public GameObject invUI, jourUI;
    private GameObject item1, questline1;
    private Vector2 delta_img;
    private List <GameObject> inv, jourD, jourUD;
    public Sprite checkmark, cross;
	public Help h;
    void Start()
    {
        input_text = GameObject.Find("input_text").GetComponent(typeof(Text)) as Text;
        pointsText = GameObject.Find("burak_counter").GetComponent(typeof(Text)) as Text;

        invUI = GameObject.Find("Inventory UI");
        jourUI = GameObject.Find("Journal UI");

        questline1 = GameObject.Find("QuestLine name");

        item1 = GameObject.Find("item");
        delta_img = item1.GetComponentInChildren<RectTransform>().sizeDelta;


        inv = new List<GameObject>();
        jourUD = new List<GameObject>();


        HideInventory();
        HideJournal();

    }

    void Update()
	{
		if (Input.GetKeyDown ("h")) 
		{
			h.ToggleHelp ();
		}
	}

    public void Points()
    {
        pointsText.text = ("Beetroots: " + points);

    }

    public void DisplayJournal(List<QuestLine> done, List<QuestLine> undone)
    {
        jourUI.SetActive(true);
        JrnContent(done,undone);

    }

    public void DisplayInventory(List<Item> items)
    {
        invUI.SetActive(true);
        InvContent(items);

    }
    public void HideJournal()
    {
       EraseJournal();

        jourUI.SetActive(false);
    }

    public void HideInventory()
    {
        EraseInventory();
        invUI.SetActive(false);
    }

   


    public void InvContent(List<Item> items, float space_width = 0.0f)
    {
        int n = 0;


        inv = new List<GameObject>();

        item1.SetActive(true);


        foreach (Item i in items)
        {
            inv.Add(Instantiate(item1, item1.transform.parent));


            Vector3 newPosition = item1.transform.position;
            newPosition.y = newPosition.y - space_width * n;
            inv[n].transform.position = newPosition;


            //         r.sprite.bounds.SetMinMax(new Vector3(-0.1f, -0.1f, 0.0f), new Vector3(0.1f, 0.1f, 0.0f));

 
            RectTransform ImgRT = inv[n].GetComponentInChildren<RectTransform>();
            //      ImgRT.sizeDelta = new Vector2(2f, 2f);


            //            SpriteRenderer r = inv[n].GetComponentInChildren<SpriteRenderer>();
            Image r = inv[n].GetComponentInChildren<Image>();

            r.sprite = i.s;

            ImgRT.sizeDelta = delta_img;
          ///  ImgRT.localScale -= new Vector3(1F, 1F, 0);



            Text t = inv[n].GetComponent<Text>();
            t.text = i.itemName;

            n++;
        }

        item1.SetActive(false);

    }

    public void EraseInventory()
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


    public void JrnContent(List<QuestLine> done, List<QuestLine> undone, float space_width = 0.0f)
    {
        int n = 0;


        jourUD = new List<GameObject>();

        questline1.SetActive(true);

        foreach (QuestLine i in undone)
        {
            jourUD.Add(Instantiate(questline1, questline1.transform.parent));


            Vector3 newPosition = questline1.transform.position;
            newPosition.y = newPosition.y - space_width * n;
            jourUD[n].transform.position = newPosition;


            Text[] t = jourUD[n].GetComponentsInChildren<Text>();
            t[0].text = i.questLineName;
            t[1].text = i.ongoing.toDo;
            Image image = jourUD[n].GetComponentInChildren<Image>();

          //  image.sprite = (cross);
            image.gameObject.SetActive(false);

            n++;
        }

        foreach (QuestLine i in done)
        {
            jourUD.Add(Instantiate(questline1, questline1.transform.parent));


            Vector3 newPosition = questline1.transform.position;
            newPosition.y = newPosition.y - space_width * n;
            jourUD[n].transform.position = newPosition;


            Text[] t = jourUD[n].GetComponentsInChildren<Text>();
            t[0].text = i.questLineName;
            t[1].text = i.ongoing.toDo;
            Image image = jourUD[n].GetComponentInChildren<Image>();
            //image.gameObject.SetActive(false);
            if(i.success)
            {
                image.sprite = (checkmark);
            }
            else
            {
                image.sprite = (cross);
            }


            n++;
        }

        questline1.SetActive(false);

    }

    public void EraseJournal()
    {

        if (jourUD.Count != 0)
        {

            foreach (GameObject i in jourUD)
            {
                if (i != null)
                {
                    Destroy(i);
                    jourUD = new List<GameObject>();
                }
            }

        }
    }

}




