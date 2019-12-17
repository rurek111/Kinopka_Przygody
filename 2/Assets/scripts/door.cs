using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

	public string LevelToLoad;
    public string message = "[E] to Enter";
   // public Scene scene;
    private game_master gm;
	private Slider slider;
	private FindInactive finder;

	void Start(){
		gm = GameObject.FindGameObjectWithTag ("game_master").GetComponent<game_master> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			gm.input_text.text = (message);
			if (Input.GetKeyDown ("e")) {
                LoadScene(LevelToLoad);


            }
        }
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			if (Input.GetKeyDown ("e")) {
                LoadScene(LevelToLoad);
            }
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			gm.input_text.text = ("");
		}

    }

    void LoadScene(string s)
    {


	
        dialogue_manager dm = FindObjectOfType<dialogue_manager>();
        dm.EndDialogue();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
       // GameObject game_master = GameObject.FindGameObjectWithTag("game_master");

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        GameObject[] gameObjectsToMove = { player, canvas };//, game_master };

        string[] namesOfGOWhereToPut0 = { "entrance_spawn", null };
        string[] namesOfGOWhereToPut1 = { "spawn_stairs", null };
        string[] where = { null, null };


        if (s == "level0")
        {
            where = namesOfGOWhereToPut0;
        }
        else if (s == "level1")
        {
            where = namesOfGOWhereToPut1;
        }
       
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetSceneByName(s), gameObjectsToMove, where));



        /*
        SceneManager.LoadScene(s);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(s));
        SceneManager.MoveGameObjectToScene(canvas, SceneManager.GetSceneByName(s));
        GameObject del = GameObject.Find("QUINCEclone");
        Destroy(del);
        del = GameObject.Find("Canvasclone");
        Destroy(del);

    */



    }


    IEnumerator LoadYourAsyncScene(Scene scene, GameObject[] objectsToLoad, string [] whereToLoad)//based on EXAPLE FROM DOCUMENTATION
    {
		FindInactive finder = FindObjectOfType<FindInactive>();
		GameObject loadingScreen  = finder.FindInactiveObjectByName("LoadingScreen");
		loadingScreen.gameObject.SetActive(true);
		GameObject slid = GameObject.Find("LoadingSlider");
		slider = slid.GetComponent<Slider> ();
		slider.value = 0;


        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelToLoad, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
			slider.value = asyncLoad.progress;
            yield return null;
        }
        int i = 0;
        foreach(GameObject g in objectsToLoad)
        {
            SceneManager.MoveGameObjectToScene(g, SceneManager.GetSceneByName(LevelToLoad));
          
        }



        foreach (GameObject g in objectsToLoad)
        {
            GameObject where = null;
            where = GameObject.Find(whereToLoad[i]);
            if (where != null)
            {
                g.transform.position = where.transform.position;
            }
            i++;

        }

        State_satisfier stateSat = GameObject.FindGameObjectWithTag("game_master").GetComponent<State_satisfier>();
        stateSat.LevelChange();



		loadingScreen.gameObject.SetActive(false);

		// Unload the previous Scene
		SceneManager.UnloadSceneAsync(currentScene);

    }
}




