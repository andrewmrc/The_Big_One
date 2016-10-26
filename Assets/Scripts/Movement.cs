using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    Animator playerAnim;
    float mouseX;
    bool isMoving;

	void Start ()
    {
        playerAnim = GetComponent<Animator>();
	}
	

	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X");


        if (isMoving)
        {
            transform.Rotate(Vector3.up, mouseX * 100f * Time.deltaTime, Space.World);
            //transform.forward = Camera.main.transform.forward;
        }
        else
        {
            transform.Rotate(Vector3.up * 0f);
        }


        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetFloat("Forward", 1f);
            isMoving = true;
        }

        else
        {
            playerAnim.SetFloat("Forward", 0.5f);
            isMoving = false;
        }


        if (Input.GetKey(KeyCode.A))
            this.transform.Rotate(transform.up, -100 * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.D))
            this.transform.Rotate(transform.up, 100 * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.S))
        {
        }
    }
}
