using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Events;
using UnityEngine.UI;

//[RequireComponent(typeof(FieldOfView))]
public class BodyControlPower : MonoBehaviour
{

    public GameObject cameraRig;
    public GameObject mainCamera;
    public bool visualiseInEditor;            // toggle for visualising the algorithm through lines for the raycast in the editor
    public UnityEvent returnEvent;

    Enemy refEnemy;
    GameManager refGM;

    private Ray m_Ray;                        // the ray used in the lateupdate for casting between the player and his target
    private RaycastHit[] m_Hits;              // the hits between the player and his target
    private RaycastHit hitInfo;
    private RayHitComparer m_RayHitComparer;  // variable to compare raycast hit distances
    private Vector3 dir;
    private bool onEnemy;
	//GameObject npcTarget;
	public float powerRange = 10;

    void Start()
    {
        refGM = FindObjectOfType<GameManager>();
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        refEnemy = FindObjectOfType<Enemy>();
        dir = transform.TransformDirection(Vector3.forward);
        // create a new RayHitComparer
        m_RayHitComparer = new RayHitComparer();
    }

    void Update()
    {
		//Tasto per attivare la mira. Quando si mira blocchiamo i movimenti del personaggio 
		//BISOGNA CREARE E POI ATTIVARE QUI IL SISTEMA DI CAMERA PER CUI SARA' POSSIBILE RUOTARE LA VISUALE MIRANDO ENTRO UN CERTO ANGOLO
		if (Input.GetMouseButton (1)) {
			this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = false;
			this.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = false;
			this.GetComponent<Animator>().SetFloat ("Forward", 0);
			this.GetComponent<Animator>().SetFloat ("Turn", 0);
			RaycastHandler ();
		} else {
			this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = true;
			this.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = true;
			onEnemy = false;
			/*
			if (npcTarget != null) {
				npcTarget.transform.GetComponent<FieldOfView> ().checkVisible = false;
				//Destroy(npcTarget.gameObject.GetComponent <FieldOfView>());
				npcTarget = null;
			}*/
		}

        
		//Se puntiamo su NPC attiviamo la UI relativa ai poteri
        if (onEnemy)
        {
            GameManager.Self.UI_Possession.SetActive(true);
        }
        else
        {
            GameManager.Self.UI_Possession.SetActive(false);
            GameManager.Self.UI_Power.SetActive(false);
			GameManager.Self.UI_Memory.SetActive(false);

        }


		//Controlliamo se questo è il corpo della protagonista oppure no e in caso attiviamo la UI e il tasto per permettere di tornare nel suo corpo
		if (this.gameObject != GameManager.Self.playerBody) {
			GameManager.Self.UI_Return.SetActive (true);
			if (Input.GetKeyDown (KeyCode.R)) {
				ReturnToYourBody ();
			}
		} else {
			GameManager.Self.UI_Return.SetActive (false);
		}


		//Potere di leggere un ricordo quando si è nel corpo di un NPC
		if (this.gameObject.transform.GetComponent<MemoryContainer> () != null) {
			GameManager.Self.UI_Hack.SetActive (true);
			if (Input.GetKey (KeyCode.F)) {
				GameManager.Self.MemoryImageUI.GetComponent<Image> ().sprite = this.gameObject.transform.GetComponent<MemoryContainer> ().memoryImage;
				GameManager.Self.MemoryImageUI.SetActive (true);
			} else {
				GameManager.Self.MemoryImageUI.GetComponent<Image> ().sprite = null;
				GameManager.Self.MemoryImageUI.SetActive (false);
			}
		} else {
			GameManager.Self.UI_Hack.SetActive (false);
		}

    }

    public void RaycastHandler()
    {
        Debug.Log("ZoomIn!");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); ;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, powerRange))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.Log(hit.collider.name + ", " + hit.collider.tag);

			//Prima controlliamo se stiamo mirando ad un personaggio controllabile
			if (hit.collider.tag == "ControllableNPC") 
            {
				//Controlliamo se il personaggio mirato abbia il componente Field Of View e in caso lo aggiungiamo
				if(hit.collider.transform.GetComponent<FieldOfView>() == null){
					Debug.Log ("Add Component Field of View");
					hit.collider.gameObject.AddComponent <FieldOfView>();
				}				

				//A questo punto controlliamo se siamo nel Field Of View del personaggio. In caso negativo possiamo usare il potere.
				if (!hit.collider.gameObject.GetComponent<FieldOfView> ().visibleTargets.Contains (this.gameObject.transform)) {
					//refEnemy.HiglightedPower();
					//Cambia emission brightness agli NPC quando puntati
					//hit.collider.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.4f,0.4f,0.4f));
					onEnemy = true;

					//Potere di dare ordini mentali. Facciamo prima un controllo così attiviamo la UI relativa solo se necessario.
					if (hit.collider.transform.GetComponent<EnemyPath> () != null) {
						GameManager.Self.UI_Power.SetActive(true);
						if (Input.GetKeyDown (KeyCode.Q)) {
							MoveNPC (hit, 0);
						} /* Per adesso non li useremo
						if (Input.GetKeyDown (KeyCode.E)) {
							MoveNPC (hit, 1);
						}
						if (Input.GetKeyDown (KeyCode.F)) {
							MoveNPC (hit, 2);
						}
						if (Input.GetKeyDown (KeyCode.T)) {
							MoveNPC (hit, 3);
						}*/
					}

					//Potere di leggere un ricordo nella mente degli NPC. Facciamo prima un controllo così attiviamo la UI relativa solo se necessario.
					if (hit.collider.transform.GetComponent<MemoryContainer> () != null) {
						GameManager.Self.UI_Memory.SetActive(true);
						if (Input.GetKey (KeyCode.F)) {
							GameManager.Self.MemoryImageUI.GetComponent<Image> ().sprite = hit.collider.transform.GetComponent<MemoryContainer> ().memoryImage;
							GameManager.Self.MemoryImageUI.SetActive (true);
						} else {
							GameManager.Self.MemoryImageUI.GetComponent<Image> ().sprite = null;
							GameManager.Self.MemoryImageUI.SetActive (false);
						}
					}


					//Potere di controllare fisicamente gli NPC
					if (Input.GetKeyDown (KeyCode.Space)) {
						this.gameObject.tag = "ControllableNPC";
						this.gameObject.transform.GetComponent<ThirdPersonUserControl> ().enabled = false;
						this.gameObject.transform.GetComponent<ThirdPersonCharacter> ().enabled = false;
						this.gameObject.transform.GetComponent<BodyControlPower> ().enabled = false;
						this.gameObject.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
						this.GetComponent<Animator>().SetFloat ("Forward", 0);
						this.GetComponent<Animator>().SetFloat ("Turn", 0);

						cameraRig.transform.GetComponent<AbstractTargetFollower> ().m_Target = null;
						hit.collider.gameObject.tag = "Player";
						hit.collider.transform.GetComponent<ThirdPersonUserControl> ().enabled = true;
						hit.collider.transform.GetComponent<ThirdPersonCharacter> ().enabled = true;
						hit.collider.transform.GetComponent<BodyControlPower> ().enabled = true;

						hit.collider.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
						hit.collider.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;

						if (hit.collider.transform.GetComponent<EnemyPath> ()) {
							hit.collider.transform.GetComponent<EnemyPath> ().enabled = false;
							hit.collider.transform.GetComponent<NavMeshAgent> ().enabled = false;
						}
						MyPosition ();
					}
				}
            }
            else
            {
                onEnemy = false;
            }
        }
    }


    public void ReturnToYourBody()
    {
        Debug.Log("Return");
        this.gameObject.tag = "ControllableNPC";
        this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
        this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
        this.gameObject.transform.GetComponent<BodyControlPower>().enabled = false;
        this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		this.GetComponent<Animator>().SetFloat ("Forward", 0);
		this.GetComponent<Animator>().SetFloat ("Turn", 0);

        cameraRig.transform.GetComponent<AbstractTargetFollower>().m_Target = null;
        GameManager.Self.playerBody.gameObject.tag = "Player";
        GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<BodyControlPower>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        MyPosition();
    }

    public void MoveNPC(RaycastHit hitted, int arrayPosition)
    {
        hitted.collider.transform.GetComponent<EnemyPath>().input = arrayPosition;
        hitted.collider.transform.GetComponent<EnemyPath>().enabled = true;
    }


    // comparer for check distances in ray cast hits
    public class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }

    void MyPosition()
    {
        returnEvent.Invoke();
    }
}
