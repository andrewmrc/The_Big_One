using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


[Serializable]
public class ArrayBool
{
	public string SequenceName;
    public bool[] sequence;
	public GameObject call;
    public bool executed;
	
    
}

[Serializable]
public class RandomArrayBool
{
    public string SequenceName;
    public bool[] sequence;
    

}
public class FlowManager : MonoBehaviour
{
    protected static FlowManager _self;
    public static FlowManager Self
    {
        get
        {
            if (_self == null)
                _self = FindObjectOfType(typeof(FlowManager)) as FlowManager;
            return _self;
        }
    }
    public List<ArrayBool> flowGameArray;
    public ArrayBool[] flowRandomGameArray;
    public int[] asd = new int[2];

    public void ExecuteRandomEvent(string sequenceName, int arrayPosition)
    {

        
        foreach (var randomArray in flowRandomGameArray)
        {
            if (sequenceName == randomArray.SequenceName)
            {
                randomArray.sequence[arrayPosition] = true;                
            }
        }
        foreach (var randomArray in flowRandomGameArray)
        {
            if (sequenceName == randomArray.SequenceName)
            {
                if (CheckAllBool(sequenceName,randomArray.sequence) && !randomArray.executed)
                {
                    randomArray.call.GetComponent<GameEvents>().ExecuteNTimes(1);
                    randomArray.executed = true;
                }
            }
        }

        
    }

    bool CheckAllBool(string sequenceName, bool[] boolArray)
    {
        bool temp = true;
        foreach (var item in boolArray)
        {
            if (!item)
            {
                temp = false;
            }

        }
        return temp;
    }
}
