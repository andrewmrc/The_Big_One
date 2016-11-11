using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor;



public class ProvaFlow : MonoBehaviour {
    public enum Condition { nothing, isTrigger, isConversation, quellochevuoi }
    public UnityEvent triggerEvent;
    public UnityEvent quelloCheVuoiEvent;
    public string normalConversation;
    public int positionInFlowArray;
    public bool isTrigger = false;

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
        if (wichCondition == Condition.isTrigger && RightPosition())
        {
            triggerEvent.Invoke();
        }
    }

    void OnTriggerStay()
    {
        if (wichCondition == Condition.quellochevuoi && RightPosition())
        {
            quelloCheVuoiEvent.Invoke();
        }
    }

    void OnTriggerExit()
    {

    }

    bool RightPosition()
    {

        if (positionInFlowArray == 0)
        {
            GameManager.Self.flowGameArray[positionInFlowArray] = true;
            return true;
            
        }
        else if (GameManager.Self.flowGameArray[positionInFlowArray-1] == true)
        {
            GameManager.Self.flowGameArray[positionInFlowArray] = true;
            return true;
        }
        return false;
    }

    public void SetBool(bool asd)
    {
        GameManager.Self.flowGameArray[positionInFlowArray] = asd;
    }

    
}
