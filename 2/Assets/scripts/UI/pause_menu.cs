using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour {

    private GameObject pause_ui;

    private bool paused = false;

    void Start() {
        //  GameObject canvas = GameObject.Find("Canvas");
        //  canvas.SetActive(true);  //
        FindInactive finder = FindObjectOfType<FindInactive>();
        pause_ui = finder.FindInactiveObjectByName("pause_ui");

        if (pause_ui != null)
        {
            pause_ui.SetActive(false);
        }
        else
        {
            Start();
            return;
        }
    }

    void Update()
    {

        if (pause_ui== null)
        {
            Start();
            return;
        }

        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            pause_ui.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            pause_ui.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void Resume()
    {
        paused = false;
    }

	public void Restart(){

		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}

	public void Main_menu(){

		SceneManager.LoadScene (0); 

	}

	public void Quit(){

		Application.Quit ();
	}
}
