using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class State_ShowMemory : State {
    UI refUI;
    
	public Sprite memoryImage;

    public override void StateUpdate()
    {
        
        Debug.LogWarning("Sto vedendo la memoria");

		refUI.HackUI (false);
		refUI.ReturnUI (false);

        refUI.cursor.GetComponent<Image>().color = Color.clear;
        refUI.MemoryImageUI(true);
		refUI.memoryImageUI.GetComponent<Image>().sprite = memoryImage;
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
