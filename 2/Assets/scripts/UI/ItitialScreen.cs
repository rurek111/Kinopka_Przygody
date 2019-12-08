using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ItitialScreen : MonoBehaviour {



	public Slider slider;


	// Use this for initialization
	void Start () {


		slider = GameObject.FindObjectOfType<Slider>();
		slider.value = 0;
		StartCoroutine (LeadToMainMenu ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public IEnumerator LeadToMainMenu()
	{
		Scene currentScene = SceneManager.GetActiveScene();

		// The Application loads the Scene in the background at the same time as the current Scene.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Additive);

		// Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			slider.value = asyncLoad.progress;
			yield return null;
		}

		// Unload the previous Scene
		SceneManager.UnloadSceneAsync(currentScene);
	}
}
