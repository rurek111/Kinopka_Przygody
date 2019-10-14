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

    public bool Compare(string name, bool requirement)
    {
        State state = states.Find(i => i.name == name);
        if(state == null)
        {
            return false;
        }
        else
        {
            if (state.satisfied == requirement)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

}


[System.Serializable]
public class State
{
    public string name;
    public bool satisfied = false;
}

[System.Serializable]
public class StateToBeChanged //changes but also exist as a prerequisite, so says how it SHOULD BE in order to smth
{
    public string statesName;
    public string name;
    public bool toBe = true;

    public bool SatifiedPrerequisite()
    {
        State_satisfier stateSat = GameObject.FindGameObjectWithTag("game_master").GetComponent<State_satisfier>();

        return stateSat.Compare(statesName, name, toBe);
    }

}