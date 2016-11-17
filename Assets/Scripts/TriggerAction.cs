using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;



public class TriggerAction : MonoBehaviour {
    public enum Condition {triggerStay, triggerEnter,triggerExit }
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;
    public string normalConversation;
    public int positionInFlowArray;
    bool executed;


    public int secondPosition = 0;

    //public bool isConversation = false;
    public Condition wichCondition;



    void OnTriggerEnter()
    {

        if (wichCondition == Condition.triggerEnter && !executed)
        {
            executed = true;
            triggerEnterEvent.Invoke();
        }
    }

    void OnTriggerStay()
    {
        if (wichCondition == Condition.triggerStay && !executed)
        {
            //if (CheckAllBool(positionInFlowArray))
            {
                executed = true;
                triggerStay.Invoke();
            }
            
        }
    }

    void OnTriggerExit()
    {
        if (wichCondition == Condition.triggerExit && !executed)
        {
            //if (CheckAllBool(positionInFlowArray))
            {
                executed = true;
                triggerExit.Invoke();
            }

        }
    }

    bool RightPosition()
    {
        List<ArrayBool> tempArrayFlow = FlowManager.Self.flowGameArray;
        if (positionInFlowArray == 0)
        {
            tempArrayFlow[positionInFlowArray].sequence[secondPosition] = true;            
            return true;
        }
        else if (CheckAllBool(positionInFlowArray-1))
        {
            tempArrayFlow[positionInFlowArray].sequence[secondPosition] = true;
            return true;
        }
        return false;
    }

    bool CheckAllBool(int positionInArray)
    {
        List<ArrayBool> tempArrayFlow = FlowManager.Self.flowGameArray;
        bool temp = true;
        
        foreach (var item in tempArrayFlow[positionInArray].sequence)
        {
            
            
            if (!item)
            {
                temp = false;
            }
            
        }
        return temp;
        
    }

    public void SetBool(bool asd)
    {
        GameManager.Self.flowGameArray[positionInFlowArray] = asd;
    }

}
