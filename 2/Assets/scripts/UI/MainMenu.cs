using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject player;
    public GameObject canvas;
	public Slider slider;
	private FindInactive finder;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGame()
    {
        StartCoroutine(LoadNewGame());
    }


     public  IEnumerator LoadNewGame()
    {
		Scene currentScene = SceneManager.GetSceneByName ("Main Menu");
		GameObject quince = GameObject.Instantiate(player);
		GameObject c = GameObject.Instantiate(canvas);
	c.gameObject.SetActive(true);
		finder = FindObjectOfType<FindInactive> ();
		GameObject loadingScreen = finder.FindInactiveObjectByName ("LoadingScreen");	
		loadingScreen.SetActive(true);

		GameObject slid = GameObject.Find("LoadingSlider");
		slider = slid.GetComponent<Slider> ();
		slider.value = 0;

		// The Application loads the Scene in the background at the same time as the current Scene.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level0", LoadSceneMode.Additive);



		// Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone)
		{
			slider.value = asyncLoad.progress;
			yield return null;
		}

		GameObject where = GameObject.Find("spawn_point");

		SceneManager.MoveGameObjectToScene(quince, SceneManager.GetSceneByName("level0"));
		SceneManager.MoveGameObjectToScene(c, SceneManager.GetSceneByName("level0"));

		quince.transform.position = where.transform.position;
		c.transform.position = where.transform.position;
		State_satisfier stateSat = GameObject.FindGameObjectWithTag("game_master").GetComponent<State_satisfier>();
		stateSat.LevelChange();

		loadingScreen.gameObject.SetActive(false);

		// Unload the previous Scene
		SceneManager.UnloadSceneAsync(currentScene);

    }

    public void Quit()
    {

        Application.Quit();
    }
}
