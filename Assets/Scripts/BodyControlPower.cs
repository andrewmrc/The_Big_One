using UnityEngine;
using System;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class BodyControlPower : MonoBehaviour {

	public GameObject cameraRig;
	public GameObject mainCamera;
	public float zoom_Speed = 6f;
	private Ray m_Ray;                        // the ray used in the lateupdate for casting between the player and his target
	private RaycastHit[] m_Hits;              // the hits between the player and his target
	private RaycastHit hitInfo;
	private RayHitComparer m_RayHitComparer;  // variable to compare raycast hit distances
	private Vector3 dir;
	public bool visualiseInEditor;            // toggle for visualising the algorithm through lines for the raycast in the editor
	private bool onEnemy;

    
    

	Enemy refEnemy;
    GameManager refGM;

	void Start ()
    {
        refGM = FindObjectOfType<GameManager>();
		cameraRig = GameObject.FindGameObjectWithTag ("CameraRig");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
        refEnemy = FindObjectOfType<Enemy>();
		dir = transform.TransformDirection(Vector3.forward);
		// create a new RayHitComparer
		m_RayHitComparer = new RayHitComparer();
	}
	
	void Update ()
    {
        
		if (Input.GetMouseButton (1)) {
			RaycastHandler ();
            

		} else {
			onEnemy = false;
            
        }

		/*
        else
        {
			Debug.Log ("ZoomOut!");
			mainCamera.transform.localPosition = new Vector3 (0.5f, -0.5f, -1f);
			//cameraRig.transform.GetComponent<ProtectCameraFromWallClip> ().enabled = true;
        }*/
		
		if (Input.GetKeyDown (KeyCode.R)) {
			ReturnToYourBody ();
		}


		if (onEnemy) {
			GameManager.Self.UI_Possession.SetActive (true);
            GameManager.Self.UI_Power.SetActive(true);
        } else {
            GameManager.Self.UI_Possession.SetActive(false);
            GameManager.Self.UI_Power.SetActive(false);
		}
	}

	public void RaycastHandler ()
    {
		Debug.Log ("ZoomIn!");
        //cameraRig.transform.GetComponent<ProtectCameraFromWallClip> ().enabled = false;
		Vector3 zoomPos = new Vector3 (0.4f, -0.4f, -0.7f);
		mainCamera.transform.localPosition = Vector3.MoveTowards (mainCamera.transform.localPosition, zoomPos, zoom_Speed * Time.deltaTime);
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));;
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100))
        {
			Debug.DrawLine (ray.origin, hit.point,Color.red );
			Debug.Log (hit.collider.name + ", " + hit.collider.tag);
			if (hit.collider.tag == "ControllableNPC") {
				//refEnemy.HiglightedPower();
				//Cambia emission brightness agli NPC quando puntati
				//hit.collider.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.4f,0.4f,0.4f));
				onEnemy = true;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    MoveNPC(hit, 0);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    MoveNPC(hit, 1);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    MoveNPC(hit, 2);
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    MoveNPC(hit, 3);
                }

                if (Input.GetKeyDown (KeyCode.Space)) {
					this.gameObject.tag = "ControllableNPC";
					this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = false;
					this.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = false;
					this.gameObject.transform.GetComponent<BodyControlPower> ().enabled = false;
					this.gameObject.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;

					Animator animPlayer = GetComponent<Animator> ();
					animPlayer.SetFloat ("Forward", 0);
                    
					cameraRig.transform.GetComponent<AbstractTargetFollower> ().m_Target = null;
					hit.collider.gameObject.tag = "Player";
					hit.collider.transform.GetComponent<ThirdPersonUserControl> ().enabled = true;
					hit.collider.transform.GetComponent<ThirdPersonCharacter> ().enabled = true;
					hit.collider.transform.GetComponent<BodyControlPower> ().enabled = true;
                    
                    hit.collider.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
					hit.collider.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
                    if (hit.collider.transform.GetComponent<EnemyPath>())
                    {
                        hit.collider.transform.GetComponent<EnemyPath>().enabled = false;
                        hit.collider.transform.GetComponent<NavMeshAgent>().enabled = false;
                    }
                    

                }

               

            } else {
				onEnemy = false;
			}
		}
	}


	public void ReturnToYourBody () {
		Debug.Log ("Return");
		this.gameObject.tag = "ControllableNPC";
		this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = false;
		this.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = false;
		this.gameObject.transform.GetComponent<BodyControlPower> ().enabled = false;
		this.gameObject.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;

		Animator animPlayer = GetComponent<Animator>();
		animPlayer.SetFloat("Forward", 0);

		cameraRig.transform.GetComponent<AbstractTargetFollower> ().m_Target = null;
		GameManager.Self.playerBody.gameObject.tag = "Player";
		GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = true;
		GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = true;
		GameManager.Self.playerBody.gameObject.transform.GetComponent<BodyControlPower> ().enabled = true;
		GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
	}

    public void MoveNPC(RaycastHit hitted,int arrayPosition)
    {
        Debug.Log(arrayPosition);
        hitted.collider.transform.GetComponent<EnemyPath>().input = arrayPosition;
        hitted.collider.transform.GetComponent<EnemyPath>().enabled = true;
        


    }


	// comparer for check distances in ray cast hits
	public class RayHitComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			return ((RaycastHit) x).distance.CompareTo(((RaycastHit) y).distance);
		}
	}
}
