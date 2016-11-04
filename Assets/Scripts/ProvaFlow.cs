using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEditor;

public enum Condition {nothing, isTrigger, isConversation, quellochevuoi }

public class ProvaFlow : MonoBehaviour {
    public UnityEvent newEvent;
    public string normalConversation;

    public bool isTrigger = false;
    //public bool isConversation = false;
    public Condition wichCondition = Condition.isTrigger;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        if (wichCondition == Condition.isTrigger)
        {
            newEvent.Invoke();
        }
    }

    void OnTriggerStay()
    {
        if (wichCondition == Condition.isConversation)
        {
            Debug.LogWarning(normalConversation);
          
        }
    }

    
}
