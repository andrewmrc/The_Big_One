﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	private AudioSource audio;
	public UnityEvent returnEvent;
	public float seconds;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		seconds = Time.time;

		if (Input.GetKeyDown (KeyCode.Space) && movie.isPlaying) {
			movie.Pause ();
			audio.Pause ();
		} else if (Input.GetKeyDown (KeyCode.Space) && movie.isPlaying){
			movie.Play ();
			audio.Play ();
		}

		if (seconds >= movie.duration) {
			returnEvent.Invoke ();
		}
	}
}
