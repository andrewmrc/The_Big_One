using UnityEngine;
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
        audio.volume = 0.4f;
    }
	
	// Update is called once per frame
	void Update () {
		seconds += Time.deltaTime;
		Debug.Log ("cutscene");
		if (Input.GetKeyDown (KeyCode.Space) && movie.isPlaying) {
			movie.Pause ();
			audio.Pause ();
		} else if (Input.GetKeyDown (KeyCode.Space) && movie.isPlaying){
			movie.Play ();
			audio.Play ();
		}
		Debug.Log (seconds + " " + movie.duration);
		if (seconds >= movie.duration) {
			
			returnEvent.Invoke ();
		}
	}
}
