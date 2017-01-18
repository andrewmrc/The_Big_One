using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AudioSource))]

public class AimingSound : MonoBehaviour {

    private AudioSource audio;
    public AudioClip otherClip;

    // Use this for initialization
    void Start () {

        audio = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
           // if (!GameManager.Self.cantUsePower)
            {
            audio.clip = otherClip;
            audio.Play();
            }

        else if (Input.GetKeyUp(KeyCode.Mouse1))
             //   if (GameManager.Self.cantUsePower)
                {
                audio.clip = otherClip;
                audio.Stop();
                }
    }
}
