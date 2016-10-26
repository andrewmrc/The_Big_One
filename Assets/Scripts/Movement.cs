using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    Animator playerAnim;
    float mouseX;
    public bool isMoving;

	void Start ()
    {
        playerAnim = GetComponent<Animator>();
	}
	

	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X");

        if (isMoving)
            transform.Rotate(Vector3.up, mouseX * 50f * Time.deltaTime, Space.World);
        else
            transform.Rotate(Vector3.up * 0f);

        if (Input.GetKey(KeyCode.W))
        {
            StartCoroutine(StartMove(-1));
            playerAnim.SetFloat("Forward", 0.5f);
            isMoving = true;
        }

        else
        {
            isMoving = false;
            playerAnim.SetFloat("Forward", 0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(MoveLateral(1));
            playerAnim.SetFloat("Forward", 0.5f);
            this.transform.Rotate(transform.up, -200 * Time.deltaTime, Space.World);
            isMoving = true;

        }

        if (Input.GetKey(KeyCode.D))
        {
            StartCoroutine(MoveLateral(-1));
            this.transform.Rotate(transform.up, 200 * Time.deltaTime, Space.World);
            playerAnim.SetFloat("Forward", 0.5f);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(StartMove(1));
            playerAnim.SetFloat("Forward", 0.5f);
            isMoving = true;
        }
    }

    IEnumerator StartMove (sbyte dir)
    {
        if (!isMoving)
        {
            transform.forward = GameObject.FindGameObjectWithTag("CameraRig").transform.forward * dir;
            yield return null;
        }
    }

    IEnumerator MoveLateral (sbyte dir)
    {
        if (!isMoving)
        {
            transform.forward = GameObject.FindGameObjectWithTag("CameraRig").transform.right * dir;
            yield return null;
        }
    }
}
