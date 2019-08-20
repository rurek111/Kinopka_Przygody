using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Transfer  {

    public Item what;
    public bool recieve;
    public Inventory who;
    private GameObject p;
    private Player player;
    private PlayerInventory yourInv;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Execute()
    {
        p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<Player>();
        yourInv = player.inventory;

        if (recieve)
        {
            who.DelItem(what);
            yourInv.AddItem(what);

        }

        else
        {
            yourInv.DelItem(what);
            who.DelItem(what);
        }
    }
}
