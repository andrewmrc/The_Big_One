﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;


//[RequireComponent(typeof(State_PowerControl))]
//[RequireComponent(typeof(State_ControlBody))]
//[RequireComponent(typeof(State_ShowMemory))]
public class FSMLogic : MonoBehaviour {

    public float powerRange = Mathf.Infinity;
    //public bool isShowMemory = false;
    public bool isAiming = false;

    //public Sprite imageSprite;


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
        
		if (Input.GetKey(KeyCode.Mouse1) && !GameManager.Self.isShowMemory)
        {
            //RaycastHandler();
            sm.HandleInput(InputTransition.MouseButtonOneDown);
            
            isAiming = true;
			UIActivator();

        }
        else
        {
            sm.HandleInput(InputTransition.MouseButtonOneUp);
            isAiming = false;
            onEnemy = false;
            UIActivator();

        }
			

		if (!isAiming && GameManager.Self.isShowMemory && Input.GetKeyDown(KeyCode.F))
		{
			Debug.Log ("Smetti Ricordo");
			UnShowMem ();
		}
		else if (GameManager.Self.outOfYourBody && this.gameObject.GetComponent<State_ShowMemory> () && !GameManager.Self.isShowMemory && !Input.GetKey(KeyCode.Mouse1)) 
		{
			if (Input.GetKeyDown (KeyCode.F) && !GameManager.Self.isShowMemory) {
				Debug.Log ("Guarda Ricordo");
				ShowMem ();

			}
			UIActivator ();
		} 



        sm.StateUpdate();


	}


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
                
                UIActivator();
            }
        }
    }



	public void ShowMem (){
		sm.HandleInput(InputTransition.ShowMemory);
		//imageSprite = hit.collider.GetComponent<MemoryContainer>().memoryImage;
		GameManager.Self.isShowMemory = true;
		//this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
		//this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
		this.gameObject.transform.GetComponent<CharController>().enabled = false;

		this.GetComponent<Animator>().SetFloat("Forward", 0);
		//this.GetComponent<Animator>().SetFloat("Turn", 0);
		Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
	}


	public void UnShowMem (){
		sm.HandleInput(InputTransition.UnshowMemory);
		//this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
		//this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
		this.gameObject.transform.GetComponent<CharController>().enabled = true;

		Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
		GameManager.Self.isShowMemory = false;

	}

    //Metodo per attivare e disattivare la UI
    void UIActivator()
    {
        if (onEnemy)
        {
            refUI.PossessionUI(true);
        }
        else
        {
            refUI.PossessionUI(false);
            refUI.PowerUI(false);
            refUI.MemoryUI(false);
            //refUI.HackUI(false);
            refUI.MemoryImageUI(false);

        }
    }
}
