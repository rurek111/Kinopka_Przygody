using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class States : MonoBehaviour
{
    public List <State> states;
		
	
}


[System.Serializable]
public class State
{
    public string name;
    public bool satisfied = true;
}