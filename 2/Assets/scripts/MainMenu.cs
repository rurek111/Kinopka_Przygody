using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject player;
    public GameObject canvas;
    


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
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject quince = GameObject.Instantiate(player);
        GameObject c = GameObject.Instantiate(canvas);
        c.gameObject.SetActive(true);


        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("level0", LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }



        GameObject where = GameObject.Find("spawn_point");


        SceneManager.MoveGameObjectToScene(quince, SceneManager.GetSceneByName("level0"));
        SceneManager.MoveGameObjectToScene(c, SceneManager.GetSceneByName("level0"));
        


        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);


        quince.transform.position = where.transform.position;
        c.transform.position = where.transform.position;
        State_satisfier stateSat = GameObject.FindGameObjectWithTag("game_master").GetComponent<State_satisfier>();
        stateSat.LevelChange();
    }

    public void Quit()
    {

        Application.Quit();
    }
}
