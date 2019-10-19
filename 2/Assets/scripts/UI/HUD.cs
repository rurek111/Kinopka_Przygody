using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private GameObject hpBar;
    private UnityEngine.UI.Slider bar;

	private Player player;

	void Start (){
	
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
        hpBar = GameObject.Find("HP bar");
        bar = hpBar.GetComponent<Slider>();


    }

    void Update (){

        //HeartUI.sprite = HeartSprites [player.currentHP];
        bar.value = player.currentHP / player.maxHP * 100;
	}


}
