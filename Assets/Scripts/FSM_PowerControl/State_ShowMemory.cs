using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class State_ShowMemory : State {
    UI refUI;
    
	public Sprite memoryImage;
	public UnityEvent returnEvent;
	public bool executed;

    public override void StateUpdate()
    {
 		refUI.HackUI (false);
		refUI.ReturnUI (false);

        refUI.cursor.GetComponent<Image>().color = Color.clear;
        refUI.MemoryImageUI(true);
		refUI.memoryImageUI.GetComponent<Image>().sprite = memoryImage;
        refUI.memoryImageUI.GetComponent<CanvasGroup>().alpha += Time.deltaTime / 2;

		if (!executed) {
			executed = true;
			returnEvent.Invoke ();
		}
        //memoryListener.Invoke();
        
    }

    // Use this for initialization
    void Start () {
        refUI = FindObjectOfType<UI>();
        SetMemoryEventIn = new UnityEvent();
        SetMemoryEventOut = new UnityEvent();
        SetAudioSource = GetComponent<AudioSource>();
        SetAudioContainer = GameManager.Self.GetComponent<AudioContainer>();

    }
	
}
