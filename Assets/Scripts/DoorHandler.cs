using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour
{

    Coroutine doorOponerCO;
    public float doorRotation;
    private Vector3 defaultRot;
    private Vector3 openRot;
    bool isClosing = false;
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

    void OnTriggerStay(Collider coll)
    {


        if (coll.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                RaycastHit hitInfo;

                Vector3 modYPlayer = new Vector3(0, 1, 0) + coll.transform.position;

                if (Physics.Raycast(modYPlayer, coll.transform.forward, out hitInfo, 1f))
                {


                    if (doorOponerCO == null)
                    {
                        //Debug.Log(transform.eulerAngles);
                        Debug.Log(doorOponerCO);
                        Debug.Log("Entro");
                        product = Vector3.Dot(transform.right, coll.transform.forward);
                        doorOponerCO = StartCoroutine(DoorOpener(product));

                    }
                }
            }


            //this.gameObject.GetComponent<BoxCollider>().enabled = false;

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
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (isOpened && !isClosing)
            {
                isClosing = true;
                if (doorOponerCO != null)
                {
                    StopCoroutine(doorOponerCO);
                }
                doorOponerCO = StartCoroutine(DoorOpener(0));
            }
            

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
            StopCoroutine(doorOponerCO);
            doorOponerCO = null;
        }
        else if (isOpened)
        {

            while (count <= 1)
            {

                count += Time.deltaTime;
                transform.eulerAngles = Vector3.Slerp(openRot, defaultRot, count);
                yield return null;
            }
            StopCoroutine(doorOponerCO);
            doorOponerCO = null;
            isOpened = false;
            isClosing = false;
        }
    }

}
