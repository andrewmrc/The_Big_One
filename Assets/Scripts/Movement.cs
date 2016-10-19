using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    Animator playerAnim;
    float mouseX;

	void Start ()
    {
        playerAnim = GetComponent<Animator>();
	}
	

	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X");
        transform.Rotate( Vector3.up, mouseX * 100f * Time.deltaTime, Space.World);


        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetFloat("Forward", 1f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetFloat("Forward", 0.5f);
        }
        else
        {
            playerAnim.SetFloat("Forward", 0.5f);
        }
    }
}
