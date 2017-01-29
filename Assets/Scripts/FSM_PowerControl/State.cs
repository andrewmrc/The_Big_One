using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class State : MonoBehaviour {

    public UnityEvent spaceListener;

    public UnityEvent SetSpaceEvent
    {
        set { spaceListener.AddListener(OnSpacePress); }
    }
    
    public abstract void StateUpdate();


    void OnSpacePress()
    {
        Debug.Log("schiacciato spazio");
    }
}
