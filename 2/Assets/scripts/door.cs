using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

	public int LevelToLoad;
	private game_master gm;

	void Start(){
		gm = GameObject.FindGameObjectWithTag ("game_master").GetComponent<game_master> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			gm.input_text.text = ("[E] to Enter");
			if (Input.GetKeyDown ("e")) {
				SceneManager.LoadScene (LevelToLoad);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			if (Input.GetKeyDown ("e")) {
				SceneManager.LoadScene (LevelToLoad);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			gm.input_text.text = (" ");
		}
	}
}
