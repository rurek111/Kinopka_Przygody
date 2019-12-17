using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    private GameObject hpBar, magicBar;
    private UnityEngine.UI.Slider hpbar, mbar;


	private Player player;

	void Start (){
	
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
        hpBar = GameObject.Find("HP bar");
		magicBar = GameObject.Find("Mana bar");

        hpbar = hpBar.GetComponent<Slider>();
		mbar = magicBar.GetComponent<Slider>();

    }

    void Update (){

        //HeartUI.sprite = HeartSprites [player.currentHP];
        hpbar.value = player.currentHP / player.maxHP * 100;
		mbar.value = player.currentMana / player.maxMana * 100;
	}


}
