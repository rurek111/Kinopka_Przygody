using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class door : MonoBehaviour {

	public string LevelToLoad;
   // public Scene scene;
	private game_master gm;

	void Start(){
		gm = GameObject.FindGameObjectWithTag ("game_master").GetComponent<game_master> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			gm.input_text.text = ("[E] to Enter");
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
       // GameObject game_master = GameObject.FindGameObjectWithTag("game_master");

        GameObject canvas = GameObject.Find("Canvas");
        GameObject[] gameObjectsToMove = { player, canvas };//, game_master };

  
        StartCoroutine(LoadYourAsyncScene(SceneManager.GetSceneByName(s), gameObjectsToMove));



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


    IEnumerator LoadYourAsyncScene(Scene scene, GameObject[] objectsToLoad)//EXAPLE FROM DOCUMENTATION
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelToLoad, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        foreach(GameObject g in objectsToLoad)
        {
            SceneManager.MoveGameObjectToScene(g, SceneManager.GetSceneByName(LevelToLoad));
        }

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}




