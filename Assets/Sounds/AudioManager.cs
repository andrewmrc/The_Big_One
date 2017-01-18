using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AudioSource))]

public class AudioManager : MonoBehaviour
{
	private AudioSource m_AudioSource;

	private bool isWalking;

	private Animator charAnimator;

	public AudioClip aimingSound;

    [Space(5)]
    public AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.

	[Header("[Tempo tra ogni suono di passo]")]
	public float stepTime;


	void Start () 
	{
		isWalking = false;
		m_AudioSource = GetComponent<AudioSource>();
		charAnimator = GetComponent<Animator> ();
	}

	void FixedUpdate () 
	{

        if (Input.GetKeyDown(KeyCode.Mouse1) && !GameManager.Self.cantUsePower)
        {
            Debug.Log("AIMING");
            m_AudioSource.clip = aimingSound;
            m_AudioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            m_AudioSource.clip = aimingSound;
            m_AudioSource.Stop();
        }

        if (charAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
		{
			if (!isWalking) {
				Debug.Log ("WALK");
				isWalking = true;
				//Facendo così la clip viene fatta partire ma non seguirà nè i movimenti precisi del personaggio nè qualche tipo di variabile temporale imposta da noi.
				//m_AudioSource.clip = otherClip;
				//m_AudioSource.Play();

				//Questa coroutine serve se vogliamo passare noi ogni quanto tempo deve sentirsi il rumore di un singolo passo.
				StartCoroutine (PlayFootStepSound (stepTime)); 
			}

		} else if(charAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))	{
			isWalking = false;
			//m_AudioSource.clip = otherClip;
			//m_AudioSource.Stop();
		}
	}


	private IEnumerator PlayFootStepSound(float waitTime) {
		while (isWalking) {
			yield return new WaitForSeconds (waitTime);
			// pick & play a random footstep sound from the array,
			// excluding sound at index 0
			int n = Random.Range (1, m_FootstepSounds.Length);
			m_AudioSource.clip = m_FootstepSounds [n];
			m_AudioSource.PlayOneShot (m_AudioSource.clip);
			// move picked sound to index 0 so it's not picked next time
			m_FootstepSounds [n] = m_FootstepSounds [0];
			m_FootstepSounds [0] = m_AudioSource.clip;
		}
	}

}
