using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;



public class ProvaFlow : MonoBehaviour {
    public enum Condition {triggerStay, triggerEnter,triggerExit }
    public UnityEvent triggerEnterEvent;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;
    public string normalConversation;
    public int positionInFlowArray;


    public int secondPosition = 0;

    //public bool isConversation = false;
    public Condition wichCondition;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {

        if (wichCondition == Condition.triggerEnter && RightPosition())
        {
            triggerEnterEvent.Invoke();
        }
    }

    void OnTriggerStay()
    {
        if (wichCondition == Condition.triggerStay && RightPosition())
        {
            if (CheckAllBool(positionInFlowArray))
            {
                triggerStay.Invoke();
            }
            
        }
    }

    void OnTriggerExit()
    {
        if (wichCondition == Condition.triggerExit && RightPosition())
        {
            if (CheckAllBool(positionInFlowArray))
            {
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
