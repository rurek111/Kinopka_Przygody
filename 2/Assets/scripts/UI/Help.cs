using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour 
{
	//private GameObject help_ui;
	private bool open = false;
	//private GameObject help;
	//public GameObject h_ui;

	// Use this for initialization
	void Start () {
		//FindInactive finder = FindObjectOfType<FindInactive>();
		//help_ui = finder.FindInactiveObjectByName("pause_ui");
		//help = GameObject.Instantiate(h_ui);
		CloseHelp ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (help == null) 
		{
			Start ();
			return;
		}
*/

	
	}


	public void ToggleHelp ()
	{
		if (open) {
			CloseHelp ();
		} 
		else if (!open) 
		{
			OpenHelp ();
		}
	}

	public void OpenHelp ()
	{
		open = true;
		Time.timeScale = 0;
		gameObject.SetActive (true);

	}

	public void CloseHelp ()
	{
		open = false;
		Time.timeScale = 1;
		gameObject.SetActive (false);
	}

}
