using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
[RequireComponent(typeof(UnityEngine.AudioSource))]

// Placeholder per IdGenerator
public class UniqueIdentifierAttribute : PropertyAttribute { }

[ExecuteInEditMode]
public class DoorHandler : MonoBehaviour
{
    //Generatore di id univoco per ogni porta
    [UniqueIdentifier]
    public string uniqueId;

    private AudioSource Audio;
    private AudioContainer soundContainer;

    // Variabile utilizzata per capire come sono direzionate
    // grazie artisti
    // False = forward == Vector3.up
    // True = forward == Vector3.forward
    public bool choosedDirection;
    // Varibili utilizzate per l'aggiunta dei collider
    Vector3 center;
    Vector3 size;
    BoxCollider qualcosa;
    public bool qualcosaInside = true;

    // Variabili designer
    [Range(2, 10)]
    public float distanceToClose = 3;
    public float doorRotation = 90;
    public List<GameObject> listOfGo;
	public List<MultiUseItem> listOfNotGo;
    public bool playerCanEnter = false;
    public bool isFreeForNpc = false;
    //public bool isLocked;
    public string[] messageList;
    public float messageSpeed = 2;
    public UnityEvent messageEvent;
	public bool setCollider = false;
	public bool executed;

    // Varibili utilizzate dal programmatore
    private GameObject player;
    private Vector3 defaultRot;
    private Vector3 openRot;
    bool npcFind = false;
    Coroutine doorOponerCO;
	public  float product;
    bool isOpened = false;
    bool isClosing = false;
	bool readingMessage = false;

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        defaultRot = transform.eulerAngles;
        qualcosaInside = true;
        Audio = GetComponent<AudioSource>();
        soundContainer = GameManager.Self.GetComponent<AudioContainer>();
    }


    // Update is called once per frame

    void Update()
    {
		if (this.gameObject.name == "Porte.020") {
			/*Debug.Log ("Forward: " + this.transform.forward);
			Debug.Log ("Up: " + this.transform.up);
			Debug.Log ("Right: " + this.transform.right);
			Debug.Log (this.transform.forward == Vector3.left/* && this.transform.up == Vector3.down*);*/

		}
		if (!setCollider) {

			if (GetComponents<BoxCollider> ().Length <= 1) {
				this.gameObject.AddComponent<BoxCollider> ();
				qualcosaInside = true;
			}

			if (qualcosaInside) {
				DestroyImmediate (this.gameObject.GetComponent<MeshCollider> ());
				qualcosaInside = false;
			}

			if (this.transform.forward == Vector3.up) {
				choosedDirection = false;
				center = new Vector3 (0.25f, 0.01f, -0.3f);
				size = new Vector3 (0.5f, 0.75f, 0.01f);


				qualcosa = GetComponents<BoxCollider> () [0];
				qualcosa.isTrigger = true;
				qualcosa.center = center;
				qualcosa.size = size;

			}

			if (this.transform.forward == Vector3.forward) {
				choosedDirection = true;
				center = new Vector3 (0.6f, -0.9f, -0.05f);
				size = new Vector3 (1.28f, 0.05f, 2f);


				qualcosa = GetComponents<BoxCollider> () [0];
				qualcosa.isTrigger = true;
				qualcosa.center = center;
				qualcosa.size = size;

			}


			if (this.transform.forward == Vector3.forward && this.transform.up == Vector3.down) {
				choosedDirection = true;
				center = new Vector3 (0.6f, -2f, -0.05f);
				size = new Vector3 (1.28f, 0.05f, 2f);

				qualcosa = GetComponents<BoxCollider> () [0];
				qualcosa.isTrigger = true;
				qualcosa.center = center;
				qualcosa.size = size;
			}


			if (this.transform.forward == Vector3.left && this.transform.up == Vector3.down) {
				choosedDirection = true;
				center = new Vector3 (0.6f, -2f, -0.05f);
				size = new Vector3 (1.28f, 0.05f, 2f);

				qualcosa = GetComponents<BoxCollider> () [0];
				qualcosa.isTrigger = true;
				qualcosa.center = center;
				qualcosa.size = size;
			}


			/*
			if (this.transform.forward == Vector3.up && this.transform.right == Vector3.forward) {

				choosedDirection = true;
				center = new Vector3 (0f, 0.6f, -2.1f);
				size = new Vector3 (2.2f, 1.25f, 0f);


				qualcosa = GetComponents<BoxCollider> () [0];
				qualcosa.isTrigger = true;
				qualcosa.center = center;
				qualcosa.size = size;
			}*/

			setCollider = true;
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
        npcFind = false;
        if (!npcFind)
        {
            foreach (var npcGO in listOfGo)
            {
                if (coll.gameObject == npcGO)
                {
                    npcFind = true;
                }
            }
        }

        if (!(coll.tag == "Player"))
        {

            player = coll.gameObject;

            foreach (var go in listOfGo)
            {
                if (coll.gameObject == go)
                {
                    if (doorOponerCO == null && !isOpened)
                    {
						if (this.GetComponent<NavMeshObstacle> ()) {
							this.GetComponent<NavMeshObstacle> ().carving = true;
						}
                        product = CalculateProduct(coll);
                        doorOponerCO = StartCoroutine(DoorOpener(product));

                    }
                }
            }

            if (isFreeForNpc)
            {
                if (doorOponerCO == null && !isOpened)
                {
					if (this.GetComponent<NavMeshObstacle> ()) {
						this.GetComponent<NavMeshObstacle> ().carving = true;
					}
                    product = CalculateProduct(coll);
                    doorOponerCO = StartCoroutine(DoorOpener(product));

                }
            }


        }

    }

    void OnTriggerStay(Collider coll)
    {
		if (coll.gameObject.tag == "Player") {
			foreach (var go in listOfGo) {
				if (coll.gameObject == go) {
					npcFind = true;
				}
			}

			player = coll.gameObject;

			if (npcFind || playerCanEnter) {
				//this.transform.GetChild (0).gameObject.SetActive (true);
				if ((Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) && !isOpened) {
					RaycastHit hitInfo;
					Vector3 modYPlayer = new Vector3 (0, 1, 0) + coll.transform.position;
					Debug.Log ("1");
					if (Physics.Raycast (modYPlayer, coll.transform.forward, out hitInfo, 2f)) {
						Debug.DrawRay (modYPlayer, coll.transform.forward, Color.magenta, 1f);
						if (doorOponerCO == null) {
							product = CalculateProduct (coll);
							doorOponerCO = StartCoroutine (DoorOpener (product));
							Audio.clip = soundContainer.OpenDoorSound;
							Audio.Play ();
						}
						/*
                        else
                        {
                            StartCoroutine(DoorMessage());
                        }
                        */
					}


				}

			} else {
				if ((Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) && !isOpened) {
					RaycastHit hitInfo;
					Vector3 modYPlayer = new Vector3 (0, 1, 0) + coll.transform.position;
					//Debug.Log ("boh");
					if (Physics.Raycast (modYPlayer, coll.transform.forward, out hitInfo, 2f)) {
						if (!readingMessage) {
							
							if (listOfNotGo.Count != null) {
								for (int i = 0; i < listOfNotGo.Count; i++) {
									if (coll.gameObject == listOfNotGo [i].npcObject) {
										//Debug.Log ("not");
										readingMessage = true;
										StartCoroutine (NewDoorMessage (i));
										//break;
									} 
									//break;
								}
							}
								
							
						}

						if (!readingMessage) {
							//Debug.Log ("yes");
							readingMessage = true;
							StartCoroutine (DoorMessage ());
						}
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
	

			/*
			for (int i = 0; i < listOfNotGo.Count; i++) {
				if (coll.gameObject == listOfNotGo [i].npcObject) {
					Debug.Log ("Trovato");
					if ((Input.GetKeyDown (KeyCode.E) || Input.GetButtonDown ("Examine")) && !isOpened) {
						RaycastHit hitInfo;
						Vector3 modYPlayer = new Vector3 (0, 1, 0) + coll.transform.position;

						if (Physics.Raycast (modYPlayer, coll.transform.forward, out hitInfo, 2f)) {
							if (!readingMessage) {
								readingMessage = true;
								StartCoroutine (NewDoorMessage (i));
							}
						}
					}
					break;
				}
				break;
			}*/


		} else {
			if (this.GetComponent<NavMeshObstacle> ()) {
				this.GetComponent<NavMeshObstacle> ().carving = true;
			}
		}
    }

    private IEnumerator DoorMessage()
    {
		//Debug.Log ("Trovato nella listOfGo");
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (true);
        //GameManager.Self.blockMovement = true;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;
       //GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);

        for (int i = 0; i < messageList.Length; i++)
        {
            GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = messageList[i];
            bool isFirstClick = false;
            float seconds = messageSpeed;
            while (seconds > 0)
            {
                seconds -= Time.deltaTime;
				if((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine")) && isFirstClick)
                {
                    seconds = 0;
                }
                isFirstClick = true;
                yield return null;
            }
        }

		if (!executed) {
			executed = true;
			messageEvent.Invoke();
		}

        GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = "";
        //GameManager.Self.blockMovement = false;
		readingMessage = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
    }


	private IEnumerator NewDoorMessage(int id)
	{
		//Debug.Log ("Trovato nella listOfNotGo");
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (true);
		//GameManager.Self.blockMovement = true;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<CharController>().enabled = false;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);

		for (int i = 0; i < listOfNotGo[id].returnMessage.Count; i++)
		{
			GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = listOfNotGo[id].returnMessage[i];
			bool isFirstClick = false;
			float seconds = messageSpeed;
			while (seconds > 0)
			{
				seconds -= Time.deltaTime;
				if((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine")) && isFirstClick)
				{
					seconds = 0;
				}
				isFirstClick = true;
				yield return null;
			}
		}

		if (!executed) {
			executed = true;
			//messageEvent.Invoke();
			listOfNotGo[id].eventToActivate.Invoke();
		}

		GameManager.Self.canvasUI.GetComponent<UI>().VariousDescriptionUI.GetComponent<Text>().text = "";
		//GameManager.Self.blockMovement = false;
		readingMessage = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Reading.SetActive (false);
	}


    private float CalculateProduct(Collider coll)
    {
        float calculatedProduct = 0;
        if (!choosedDirection)
        {
			//Debug.Log ("asd");
			calculatedProduct = Vector3.Dot(-transform.up, coll.transform.forward);
        }
        if (choosedDirection)
        {
			
            calculatedProduct = Vector3.Dot(transform.forward, coll.transform.forward);
        }
        return calculatedProduct;
    }

    void OnTriggerExit(Collider other)
    {
		//this.transform.GetChild (0).gameObject.SetActive (true);
		if (this.GetComponent<NavMeshObstacle> ()) {
			this.GetComponent<NavMeshObstacle> ().carving = false;
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
            Audio.clip = soundContainer.CloseDoorSound;
            Audio.Play();

        }
    }

    public void SetPlayerCanEnter(bool change)
    {
        playerCanEnter = change;
    }

    public void AddOjbect(GameObject otherGo)
    {
        if (!listOfGo.Contains(otherGo))
        {
            listOfGo.Add(otherGo);
        }

		//Vedere se si può fare in maniera migliore
		//Se vede che il personaggio da aggiungere alla listOfGo è presente in quella di notGo lo rimuove da quest'ultima
		for (int i = 0; i < listOfNotGo.Count; i++) {
			if (listOfNotGo[i].npcObject.Equals (otherGo)) {
				listOfNotGo [i].npcObject = null; 
			}
		}

    }


    public void SetFreeNpc(bool change)
    {
        isFreeForNpc = change;
    }


	public void ResetExecuted () {
		executed = false;
	}

}
