using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class DialogueItem {

	public GameObject npcSpeaker;
	public List<string> dialogues;
	public List<AudioClip> audioSpeech;
	public bool executed;
	public UnityEvent eventToActivate;
}
