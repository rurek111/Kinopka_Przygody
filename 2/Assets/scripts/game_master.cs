using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_master : MonoBehaviour {

	public int points;
	public Text pointsText;
	public Text input_text;

	void Update(){
		pointsText.text = ("Points: " + points);
	}

}
