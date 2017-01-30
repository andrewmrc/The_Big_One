using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class State : MonoBehaviour {
    [HideInInspector]
    public UnityEvent spaceListener;
    [HideInInspector]
    public UnityEvent rListener;
    [HideInInspector]
    public UnityEvent ideaListener;
    [HideInInspector]
    public UnityEvent memoryListenerIn;
    [HideInInspector]
    public UnityEvent memoryListenerOut;
    [HideInInspector]
    public UnityEvent SetSpaceEvent
    {
        set { spaceListener.AddListener(OnSpacePress); }
    }

    public UnityEvent SetReturnEvent
    {
        set { rListener.AddListener(OnReturnPress); }
    }

    public UnityEvent SetIdeaEvent
    {
        set { ideaListener.AddListener(OnIdeaPress); }
    }

    public UnityEvent SetMemoryEventIn
    {
        set { memoryListenerIn.AddListener(OnMemoryPressIn); }
    }

    public UnityEvent SetMemoryEventOut
    {
        set { memoryListenerOut.AddListener(OnMemoryPressOut); }
    }

    public abstract void StateUpdate();


    void OnSpacePress()
    {
        // Quando schiacci spazio fai cose
        Debug.Log("schiacci spazio");
    }

    void OnReturnPress()
    {
        // Quando ritorni nel tuo corpo fai cose
        Debug.Log("schiacci r");
    }

    void OnIdeaPress()
    {
        // Quando imprimi un'idea fai cose
        Debug.Log("schiacci la q");
    }
    
    void OnMemoryPressIn()
    {
        // Quando guardi un ricordo fai cose
        Debug.Log("schiacci la f");
    }

    void OnMemoryPressOut()
    {
        // Quando smetti di guardare un ricordo fai cose
        Debug.Log("schiacci la f");
    }
}
