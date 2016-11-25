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


    #region Check and Execute Random Event
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
                if (CheckAllBool(randomArray.sequence) && !randomArray.executed)
                {
                    randomArray.call.GetComponent<GameEvents>().ExecuteNTimes(1);
                    randomArray.executed = true;
                }
            }
        }        
    }

    bool CheckAllBool(bool[] boolArray)
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
    #endregion

    public void ExecuteSequenceEvent(string sequenceName, int arrayPosition)
    {
        for (int i = 0; i < flowGameArray.Count; i++)
        {
            if (flowGameArray[i].SequenceName == sequenceName)
            {
                if (i == 0)
                {
                    
                    if (arrayPosition == 0)
                    {
                        flowGameArray[i].sequence[arrayPosition] = true;
                    }
                    else
                    {
                        if(flowGameArray[i].sequence[arrayPosition-1])
                        {
                            flowGameArray[i].sequence[arrayPosition] = true;
                        }
                    }
                }
                else
                {
                    if (CheckAllBool(flowGameArray[i-1].sequence))
                    {
                        if (arrayPosition == 0)
                        {
                            flowGameArray[i].sequence[arrayPosition] = true;
                        }
                        else
                        {
                            if (flowGameArray[i].sequence[arrayPosition - 1])
                            {
                                flowGameArray[i].sequence[arrayPosition] = true;
                            }
                        }
                    }
                }
            }    
        }
        foreach (var gameArray in flowGameArray)
        {
            if (sequenceName == gameArray.SequenceName)
            {
                if (CheckAllBool(gameArray.sequence) && !gameArray.executed)
                {
                    
                    gameArray.call.GetComponent<GameEvents>().ExecuteNTimes(1);
                    gameArray.executed = true;
                }
            }
        }
    }
}
