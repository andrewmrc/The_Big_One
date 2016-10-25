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
        refUI.MemoryImageUIHand(true);
        refUI.memoryImageUI.GetComponent<Image>().sprite = GetComponent<FSMLogic>().imageSprite;
        refUI.memoryImageUI.GetComponent<CanvasGroup>().alpha += Time.deltaTime / 2;


        
    }

    // Use this for initialization
    void Start () {
        refUI = FindObjectOfType<UI>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
