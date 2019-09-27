using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class State_satisfier : MonoBehaviour {
    public List <States> allStates;

    // Use this for initialization
    void Start () {
        States [] temp  = FindObjectsOfType<States>();
        allStates = temp.ToList();


    }


    // Update is called once per frame
    void Update () {
		

	}

    public void Satisfy(string statesName, string stateName)
    {
        States s = allStates.Find(i => i.statesName == statesName);
        s.Satisfy(stateName);
    }

    public void Set(string statesName, string stateName, bool toWhat)
    {
        States s = allStates.Find( i => i.statesName == statesName);
        if(s!=null)
        {
            if (toWhat)
            {
                s.Satisfy(stateName);

            }
            else
            {
                s.Dissatisfy(stateName);

            }
        }
       
    }

    public bool Compare(string statesName, string stateName, bool requirement)
    {
        States s = allStates.Find(i => i.statesName == statesName);
        return s.Compare( stateName,  requirement);

    }
}


//satisfaction by transfer, possession, simplyVlue(Type,name, where, value), sentence, existance, visibility
