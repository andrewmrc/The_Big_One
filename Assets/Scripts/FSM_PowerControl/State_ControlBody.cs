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

        #region Player Control check
        //Controlliamo se questo è il corpo della protagonista oppure no e in caso attiviamo la UI e il tasto per permettere di tornare nel suo corpo
        if (this.gameObject != GameManager.Self.playerBody)
        {
            GameManager.Self.outOfYourBody = true;
            refUI.ReturnUI(true);
            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Return"))
            {
                ReturnToYourBody();
            }
        }
        else
        {
            refUI.ReturnUI(false);
            GameManager.Self.outOfYourBody = false;
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
        this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
        this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
        this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Animator>().SetFloat("Forward", 0);
        this.GetComponent<Animator>().SetFloat("Turn", 0);

        cameraRig.transform.GetComponent<AbstractTargetFollower>().m_Target = null;
        GameManager.Self.playerBody.gameObject.tag = "Player";
        GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<FSMLogic>().enabled = true;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameManager.Self.playerBody.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        MyPosition();
    }

    void MyPosition()
    {
        
        returnEvent.Invoke();

        
                 
    }
}
