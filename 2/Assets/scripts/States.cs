using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class States : MonoBehaviour
{
    public string statesName; // more or less what states are being store here

    public List <State> states;
	



    public void Satisfy(string name)
    {
        State state = states.Find(i => i.name == name);
        state.satisfied = true;
    }

    public void Dissatisfy(string name)
    {
        State state = states.Find(i => i.name == name);
        state.satisfied = false;
    }
    public void Toggle(string name)
    {
        State state = states.Find(i => i.name == name);
        state.satisfied = !state.satisfied;
    }

}


[System.Serializable]
public class State
{
    public string name;
    public bool satisfied = false;
}

[System.Serializable]
public class StateToBeChanged
{
    public string statesName;
    public string name;
    public bool toBe = true;
}