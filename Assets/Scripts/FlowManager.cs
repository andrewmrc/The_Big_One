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
                if (CheckAllBool(flowGameArray[i - 1].sequence))
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

    public void ExecuteNewSequenceEvent(int sequencePosition,int actionPosition)
    {
        // Controllo se è la prima sequenza nella lista
        if (sequencePosition == 0)
        {
            // Controllo se è la prima azione nella lista
            if (actionPosition == 0)
            {
                flowGameArray[sequencePosition].sequence[actionPosition] = true;
            }
            // Se non è la prima azione
            else
            {
                // Controllo se l'azione precedente è vera
                if (flowGameArray[sequencePosition].sequence[actionPosition - 1])
                {
                    flowGameArray[sequencePosition].sequence[actionPosition] = true;
                }
            }
        }
        // Se non è la prima sequenza controllo se la precedente è stata eseguita
        else if(flowGameArray[sequencePosition - 1].executed)
        {         
            if (actionPosition == 0)
            {
                flowGameArray[sequencePosition].sequence[actionPosition] = true;
            }
            else
            {
                if (flowGameArray[sequencePosition].sequence[actionPosition - 1])
                {
                    flowGameArray[sequencePosition].sequence[actionPosition] = true;
                }
            }
        }

        if (CheckAllBool(flowGameArray[sequencePosition].sequence))
        {
            if (flowGameArray[sequencePosition].call)
            {
                flowGameArray[sequencePosition].call.GetComponent<GameEvents>().ExecuteNTimes(1);
            }
            else
            {
                Debug.LogError("Hai dimenticato l'azione da mettere nel FlowManager nella sequence : " + flowRandomGameArray[sequencePosition].SequenceName);
            }

            flowGameArray[sequencePosition].executed = true;
        }
    }

    public void ExecuteNewRandomEvent(int sequencePosition)
    {

        if (!flowRandomGameArray[sequencePosition].executed)
        {
            for (int i = 0; i < flowRandomGameArray[sequencePosition].sequence.Length; i++)
            {
                if (!flowRandomGameArray[sequencePosition].sequence[i])
                {
                    flowRandomGameArray[sequencePosition].sequence[i] = true;
                    break;

                }
            }
            if (CheckAllBool(flowRandomGameArray[sequencePosition].sequence))
            {
                if (flowRandomGameArray[sequencePosition].call)
                {
                    flowRandomGameArray[sequencePosition].call.GetComponent<GameEvents>().ExecuteNTimes(1);
                }
                else
                {
                    Debug.LogError("Hai dimenticato l'azione da mettere nel FlowManager nella sequence : " + flowRandomGameArray[sequencePosition].SequenceName);
                }

                flowRandomGameArray[sequencePosition].executed = true;
            }
        }
    }
}
