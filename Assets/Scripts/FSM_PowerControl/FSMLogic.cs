using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Events;


//[RequireComponent(typeof(State_PowerControl))]
//[RequireComponent(typeof(State_ControlBody))]
//[RequireComponent(typeof(State_ShowMemory))]
public class FSMLogic : MonoBehaviour {

    public float powerRange = 7f;
    //public bool isShowMemory = false;
    public bool isAiming = false;

    //public Sprite imageSprite;

	float currentWeight = 0f;
    
    public bool onEnemy = false;
    UI refUI;

	private StateMachine sm;

	// Use this for initialization
	void Start () {
        refUI = FindObjectOfType<UI>();

        sm = new StateMachine();
        sm.stateControlBody = GetComponentInChildren<State_ControlBody>();
        sm.statePowerControl = GetComponentInChildren<State_PowerControl>();
        sm.stateShowMemory = GetComponentInChildren<State_ShowMemory>();

        // Initialize the first state and create the transitions
        sm.initialState = sm.stateControlBody;
        sm.StartMachine();
        sm.CreateTransition();

    }
	
	// Update is called once per frame
	void Update () {
        

		if (!GameManager.Self.isShowMemory) {
			if ((Input.GetMouseButton (1) || (Input.GetAxis ("LeftTriggerJoystick") >= 0.001))/*!GameManager.Self.cantUsePower */) {
				if ((GameManager.Self.ChangePlayerState == GameState.UsePower || GameManager.Self.ChangePlayerState == GameState.OnlyIdea)) {
					//RaycastHandler();
					sm.HandleInput (InputTransition.MouseButtonOneDown);
					GameManager.Self.mainCamera.GetComponent<CameraFilterPack_Vision_Tunnel> ().enabled = true;
					isAiming = true;
					UIActivator ();
					//AnimationActivator ();
				}
			} else {
				sm.HandleInput (InputTransition.MouseButtonOneUp);
				GameManager.Self.mainCamera.GetComponent<CameraFilterPack_Vision_Tunnel> ().enabled = false;
				isAiming = false;
				onEnemy = false;
				UIActivator ();
				//AnimationActivator ();
			}
		}

		if (!isAiming && GameManager.Self.isShowMemory && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Hack")))
		{
            //Debug.Log ("Smetti Ricordo");
            //sm.stateShowMemory.memoryListenerOut.Invoke();
            UnShowMem ();
		}
		else if (!isAiming && GameManager.Self.outOfYourBody && this.gameObject.GetComponent<State_ShowMemory> () && this.gameObject.GetComponent<State_ShowMemory> ().enabled && !GameManager.Self.isShowMemory && !Input.GetKey(KeyCode.Mouse1)) 
		{
			if ((Input.GetKeyDown (KeyCode.F) || Input.GetButtonDown("Hack")) && !GameManager.Self.isShowMemory) {
                //Debug.Log ("Guarda Ricordo");
                //sm.stateShowMemory.memoryListenerIn.Invoke();
				//GameManager.Self.isShowMemory = true;
				ShowMem ();

			}
			UIActivator ();
		} 



        sm.StateUpdate();


	}

	/*
    public void RaycastHandler()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, powerRange))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.black);
            if (hit.collider.tag == "ControllableNPC")
            {
                #region Memory condition
                if (hit.collider.GetComponent<MemoryContainer>())
                {
                    refUI.MemoryUI(true);
					if (Input.GetKeyDown(KeyCode.F) && !GameManager.Self.isShowMemory)
                    {                        
						ShowMem ();
                    }
					else if (Input.GetKeyDown(KeyCode.F) && GameManager.Self.isShowMemory)
                    {                        
                        UnShowMem ();
                    }

                }
                #endregion
                UIActivator();
            }
            else
            {
				refUI.PowerUI(false);
                UIActivator();
            }
        }
    }*/



	public void ShowMem (){
		sm.HandleInput(InputTransition.ShowMemory);
		//imageSprite = hit.collider.GetComponent<MemoryContainer>().memoryImage;
		GameManager.Self.isShowMemory = true;
		//this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
		//this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
		this.gameObject.transform.GetComponent<CharController>().enabled = false;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Esci_Ricordo.SetActive (true);
		this.GetComponent<Animator>().SetFloat("Forward", 0);
		//this.GetComponent<Animator>().SetFloat("Turn", 0);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
		Camera.main.GetComponentInParent<CameraFilterPack_TV_VHS>().enabled = true;

	}


	public void UnShowMem (){
		sm.HandleInput(InputTransition.UnshowMemory);
		//this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
		//this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
		this.gameObject.transform.GetComponent<CharController>().enabled = true;
		GameManager.Self.canvasUI.GetComponent<UI> ().UI_Esci_Ricordo.SetActive (false);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
		GameManager.Self.isShowMemory = false;
		Camera.main.GetComponentInParent<CameraFilterPack_TV_VHS>().enabled = false;
	}

    //Metodo per attivare e disattivare la UI
    void UIActivator()
    {
		if (GameManager.Self.playerState == GameState.UsePower && onEnemy) {
			refUI.PossessionUI (true);
			//refUI.PowerUI(true);
			//refUI.TastiUI(true);
		} else if (GameManager.Self.playerState == GameState.OnlyIdea && onEnemy) {
			refUI.PowerUI (true);
		} else {
			
            //refUI.TastiUI(false);
            refUI.PossessionUI(false);
            refUI.PowerUI(false);
            //refUI.MemoryUI(false);
            //refUI.HackUI(false);
            refUI.MemoryImageUI(false);

        }
    }


	void AnimationActivator () {
		//if(this.gameObject.GetComponent<Animator>().GetLayerIndex("BaseLayer").CompareTo
		if (isAiming) {
			//this.gameObject.GetComponent<Animator> ().SetLayerWeight (1, 0.4f);
			currentWeight = Mathf.Lerp(currentWeight, 0.5f, Time.deltaTime * 3f);
			this.gameObject.GetComponent<Animator> ().SetLayerWeight(1, currentWeight);
		} else {
			//this.gameObject.GetComponent<Animator> ().SetLayerWeight (1, 0f);
			currentWeight = Mathf.Lerp(currentWeight, 0f, Time.deltaTime * 4f);
			this.gameObject.GetComponent<Animator> ().SetLayerWeight(1, currentWeight);
		}
	}
}
