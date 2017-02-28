using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class MultiUseItem {

	public GameObject npcObject;
	public List<string> returnMessage;
	//public List<AudioClip> audioSpeech;
	//public bool executed;
	public UnityEvent eventToActivate;
}
