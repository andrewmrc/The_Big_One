using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_ControlBody : State {

    public UnityEvent returnEvent;
    UI refUI;
    public GameObject cameraRig;

    public override void StateUpdate()
    {
        Debug.LogWarning("Non premi il tasto destro");
        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = enabled;
        //this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = enabled;
		if (!GameManager.Self.blockMovement) {
			this.gameObject.transform.GetComponent<CharController> ().enabled = enabled;
		}

        refUI.cursorFar.SetActive(false);

		#region Player Control check
		//Controlliamo se questo è il corpo della protagonista oppure no e in caso attiviamo la UI e il tasto per permettere di tornare nel suo corpo
		if (GameManager.Self.outOfYourBody)
		{
			refUI.ReturnUI(true);
			if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Return"))
			{
				ReturnToYourBody();
			}
		}
        #endregion

		#region Memory check
		if (this.gameObject.GetComponent<State_ShowMemory> ()) {
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
    }


    public void ReturnToYourBody()
    {
        MyGlobal.ChangeBody(GameManager.Self.playerBody.gameObject);
        Debug.Log("Return");
        this.gameObject.tag = "ControllableNPC";
        this.gameObject.transform.GetComponent<FSMLogic>().enabled = false;
        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
       // this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;

		this.gameObject.transform.GetComponent<CharController>().enabled = false;

        this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Animator>().SetFloat("Forward", 0);
        this.GetComponent<Animator>().SetFloat("Turn", 0);

		if(this.GetComponent<DialogueHandler>()){
			this.GetComponent<DialogueHandler>().cantTalk = false;
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

		MyPosition();

    }


    void MyPosition()
    {
        
        returnEvent.Invoke();
        
    }
}
