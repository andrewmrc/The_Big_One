using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class State_ShowMemory : State {
    UI refUI;
    public override void StateUpdate()
    {
        Debug.LogWarning("Sto vedendo la memoria");
        refUI.cursor.GetComponent<Image>().color = Color.clear;

    }

    // Use this for initialization
    void Start () {
        refUI = FindObjectOfType<UI>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
