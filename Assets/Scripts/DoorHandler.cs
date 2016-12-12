using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour
{

    public float doorRotation;
    private Vector3 defaultRot;
    private Vector3 openRot;
    [Range(0, 1)]
    public float speed = 0.2f;

    public float product;
    bool isOpened = false;

    // Use this for initialization
    void Start()
    {
        defaultRot = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //this.gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log(transform.eulerAngles);


            product = Vector3.Dot(transform.forward, coll.transform.forward);
            StartCoroutine(DoorOpener(product));
            /*Debug.Log("APRI PORTA");

			ContactPoint contact = coll.contacts[0];
			Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
			Vector3 pos = contact.point;

			Debug.Log (coll.contacts [0].point);
			Debug.Log ("ROT: " + rot);
			//this.gameObject.transform.rotation = Quaternion.Euler (this.gameObject.transform.rotation.x - 90f, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z);
			this.gameObject.transform.rotation = rot;*/



        }
    }

    IEnumerator DoorOpener(float product)
    {
        float count = 0;
        if (!isOpened)
        {
            if (product >= 0)
            {

                openRot = new Vector3(defaultRot.x, defaultRot.y - doorRotation, defaultRot.z);
            }
            else if (product < 0)
            {
                openRot = new Vector3(defaultRot.x, defaultRot.y + doorRotation, defaultRot.z);
            }

            isOpened = true;

            while (count <= 1)
            {

                count += Time.deltaTime;
                transform.eulerAngles = Vector3.Slerp(defaultRot, openRot, count);
                yield return null;
            }
        }
        else if (isOpened)
        {

        }
    }

}
