using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindInactive : MonoBehaviour { //algorithms taken from https://stackoverflow.com/questions/44456133/find-inactive-gameobject-by-name-tag-or-layer

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public  GameObject FindInactiveObjectByName(string name)//this one. (the only one used)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    public GameObject FindInactiveObjectByLayer(int layer)//this also
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].gameObject.layer == layer)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }


    public GameObject FindInActiveObjectByTag(string tag)//and this.
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

}
