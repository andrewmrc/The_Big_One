using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_ControlBody : State {

    public UnityEvent returnEvent;
	//public bool executed;
    UI refUI;
    public GameObject cameraRig;

   
    public override void StateUpdate()
    {
        //Debug.LogWarning("Non premi il tasto destro");
        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = enabled;
        //this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = enabled;
		if (!GameManager.Self.blockMovement) {
			if(this.gameObject.transform.GetComponent<CharController> ()){
				this.gameObject.transform.GetComponent<CharController> ().enabled = enabled;
			}
		}

        refUI.cursorFar.SetActive(false);

		#region Player Control check
		//Controlliamo se questo è il corpo della protagonista oppure no e in caso attiviamo la UI e il tasto per permettere di tornare nel suo corpo
		if (GameManager.Self.outOfYourBody)
		{
			refUI.ReturnUI(true);
			if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Return"))
			{                
                //rListener.Invoke();
				ReturnToYourBody();
			}
		}
        #endregion

		#region Memory check
		if (this.gameObject.GetComponent<State_ShowMemory> () && this.gameObject.GetComponent<State_ShowMemory> ().enabled) {
			refUI.HackUI (true);

		} else {
			refUI.HackUI (false);

		}
		#endregion
    }


    // Use this for initialization
    void Start () {
        refUI = FindObjectOfType<UI>();
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        SetReturnEvent = new UnityEvent();
        SetAudioSource = GetComponent<AudioSource>();
        SetAudioContainer = GameManager.Self.GetComponent<AudioContainer>();
    }


    public void ReturnToYourBody()
    {
        MyGlobal.ChangeBody(GameManager.Self.playerBody.gameObject);
        //Debug.Log("Return");
		Camera.main.GetComponentInParent<CameraFilterPack_TV_VHS_Rewind>().enabled = true;
        this.gameObject.tag = "ControllableNPC";
        this.gameObject.transform.GetComponent<FSMLogic>().enabled = false;
        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
       // this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;

		if(this.gameObject.transform.GetComponent<CharController> ()){
			this.gameObject.transform.GetComponent<CharController> ().enabled = false;
			this.gameObject.transform.GetComponent<CharController>().stayThere = false;
		}

        this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Animator>().SetFloat("Forward", 0);
        this.GetComponent<Animator>().SetFloat("Turn", 0);
		this.GetComponent<Animator>().SetBool ("Collision", false);

		if(this.GetComponent<DialogueHandler>()){
			this.GetComponent<DialogueHandler>().cantTalk = false;
		}

        // controllo patrolling e isPatrolling è true allora fai partire coroutine
        if (this.gameObject.GetComponent<Patrolling>() && this.gameObject.GetComponent<Patrolling>().isPatrolling)
        {
            this.gameObject.GetComponent<Patrolling>().StartNStopPatrolling(true);
        }

        cameraRig.transform.GetComponent<AbstractTargetFollower>().m_Target = null;
        GameManager.Self.playerBody.gameObject.tag = "Player";
        //GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
        //GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;

		GameManager.Self.playerBody.gameObject.transform.GetComponent<CharController>().enabled = true;

        GameManager.Self.playerBody.gameObject.transform.GetComponent<FSMLogic>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		GameManager.Self.outOfYourBody = false;
		refUI.ReturnUI(false);
		GameManager.Self.StopCameraEffect();
		MyPosition();

    }


    void MyPosition()
    {
		returnEvent.Invoke ();
		/*
		if (!executed) {
			executed = true;
			returnEvent.Invoke ();
		}*/
        
    }
}
