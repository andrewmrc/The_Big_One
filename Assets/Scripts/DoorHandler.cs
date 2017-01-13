using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
public class DoorHandler : MonoBehaviour
{

    Coroutine doorOponerCO;
    public float doorRotation = 90;
    private Vector3 defaultRot;
    private Vector3 openRot;
    bool isClosing = false;
    [Range(0, 1)]
    public float speed = 0.2f;

    public float product;
    bool isOpened = false;
    BoxCollider qualcosa;

    Vector3 center;
    Vector3 size;

    private GameObject player;
    [Range(3, 10)]
    public float distanceToClose = 3;

    public bool isNameContains = false;
    public string nameContains;

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        defaultRot = transform.eulerAngles;

    }


    // Update is called once per frame

    void Update()
    {
        center = new Vector3(0, 0.15f, -0.3f);
        size = new Vector3(1, 0.32f, 0.01f);
        if (!this.gameObject.GetComponent<BoxCollider>())
        {

            qualcosa = this.gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            qualcosa = this.gameObject.GetComponent<BoxCollider>();
            qualcosa.isTrigger = true;
            qualcosa.center = center;
            qualcosa.size = size;
        }

        if (player)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) > distanceToClose)
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
    }

    void OnTriggerEnter(Collider coll)
    {
        
        if (!(coll.tag == "Player"))
        {

            player = coll.gameObject;
            if (!isNameContains)
            {
                
                if (doorOponerCO == null)
                {
                    product = Vector3.Dot(transform.right, coll.transform.forward);
                    doorOponerCO = StartCoroutine(DoorOpener(product));

                }
            }
            else if (nameContains != "" && coll.name.Contains(nameContains))
            {
                
                if (doorOponerCO == null)
                {
                    product = Vector3.Dot(transform.right, coll.transform.forward);
                    doorOponerCO = StartCoroutine(DoorOpener(product));

                }
            }
        }
        
        
    }

    void OnTriggerStay(Collider coll)
    {


        if (coll.gameObject.tag == "Player")
        {
            player = coll.gameObject;
            if (Input.GetKeyDown(KeyCode.C))
            {
                RaycastHit hitInfo;

                Vector3 modYPlayer = new Vector3(0, 1, 0) + coll.transform.position;

                if (Physics.Raycast(modYPlayer, coll.transform.forward, out hitInfo, 2f))
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
                transform.eulerAngles = Vector3.Lerp(defaultRot, openRot, count);
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
                transform.eulerAngles = Vector3.Lerp(openRot, defaultRot, count);
                yield return null;
            }
            StopCoroutine(doorOponerCO);
            doorOponerCO = null;
            isOpened = false;
            isClosing = false;
        }
    }

}
