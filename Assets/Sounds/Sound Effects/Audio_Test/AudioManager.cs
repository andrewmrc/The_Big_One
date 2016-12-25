using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AudioSource))]
public class AudioManager : MonoBehaviour
{
	public AudioClip otherClip;
	public Animator OliviaAnimator;
	private bool Walk;

	void Start () 
	{
		Walk = false;
	}

	void Update () 
	{
		AudioSource audio = GetComponent<AudioSource> ();

		if (OliviaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
			{
				Walk = true;
				audio.clip = otherClip;
				audio.Play();
			}


		if (OliviaAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
					Walk = false;
					audio.clip = otherClip;
					audio.Stop();
			}
	}
}
