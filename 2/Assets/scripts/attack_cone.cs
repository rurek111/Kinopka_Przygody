using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_cone : MonoBehaviour {

	public turretAI turret_ai;
	public bool isLeft = false;


	void Awake(){
		turret_ai = gameObject.GetComponentInParent<turretAI> ();
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			if (isLeft) {
				turret_ai.attack (false);
			} 
			else
			{
				turret_ai.attack (true);
			}
		}
	}
}