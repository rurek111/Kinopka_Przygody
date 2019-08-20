﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Player Player;

	void Start(){
	
		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

	}

	void OnTriggerEnter2D(Collider2D col){
	
		if (col.CompareTag ("Player")) {
		
			Player.Damage (1);

			Player.Knockback (0.02f, 700, Player.transform.position);
		}
	
	}

}
