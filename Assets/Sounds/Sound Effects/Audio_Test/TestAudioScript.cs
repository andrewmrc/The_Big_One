using UnityEngine;
using System.Collections;
[RequireComponent(typeof(UnityEngine.AudioSource))]

public class TestAudioScript : MonoBehaviour {

    public AudioClip insertClip;
    private AudioSource audioS;
    private Animator charAnimator;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
        charAnimator = transform.root.GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider col)
    {
        Debug.Log("Collido!!!" + transform.root);
        
        if (col.gameObject.tag == "Floor")
        {
            Debug.Log("CollidoFloor");
            if (charAnimator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                audioS.PlayOneShot(insertClip);
            }
        }

    }
}
