using UnityEngine;
using System.Collections;

public class CrouchScript : MonoBehaviour {
    [Range(0,4)]
    public float speed = 1f;
    public int i;
    // Use this for initialization
    void Start () {
        StartCoroutine(Crouch());
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator Crouch ()
    {
        i = 0;
        while (true)
        {
            
            
            if (i == 0)
            {
                GetComponent<Animator>().SetBool("Crouch", true);
                i++;
            }
            else
            {
                GetComponent<Animator>().SetBool("Crouch", false);
                i--;
            }
            yield return new WaitForSeconds(speed);

        }
    }
}
