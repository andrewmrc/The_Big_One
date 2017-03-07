using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AudioSource))]

public class ElevatorSound : MonoBehaviour {

	private AudioSource Audio;
	private AudioContainer soundContainer;

	// Use this for initialization
	void Start () {
		soundContainer = GameManager.Self.GetComponent<AudioContainer>();
		Audio = GetComponent<AudioSource>();
		Audio.clip = soundContainer.ElevatorSound;
		Audio.volume = 0.1f;
		Audio.PlayOneShot (Audio.clip);

	}
	

}
