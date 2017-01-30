using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class State_PowerControl : State {

    public GameObject cameraRig;
    public GameObject mainCamera;


    public bool isNear = false; 
    public float powerRange = 10f;
    public int controlBodyCost = 20;
    public int mentalPowerCost = 10;

    public Sprite cursorPoint;
    public Sprite cursorFar;



    UI refUI;

    void Start()
    {
        refUI = FindObjectOfType<UI>();
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cursorPoint = Resources.Load("CursorPoint") as Sprite;
        cursorFar = Resources.Load("CursorFar") as Sprite;
		SetSpaceEvent = new UnityEvent ();
        SetIdeaEvent = new UnityEvent();
        SetAudioSource = GetComponent<AudioSource>();
        SetAudioContainer = GameManager.Self.GetComponent<AudioContainer>();
    }

    public override void StateUpdate()
    {
        
        //Debug.LogWarning("Premi il tasto destro");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        refUI.memoryImageUI.GetComponent<CanvasGroup>().alpha = 0;
        refUI.ReturnUI(false);
		refUI.HackUI (false);
        this.GetComponent<Animator>().SetFloat("Forward", 0);
        this.GetComponent<Animator>().SetFloat("Turn", 0);
        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
        //this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
		this.gameObject.transform.GetComponent<CharController>().enabled = false;

        refUI.cursorFar.SetActive(false);

        // Controlliamo se l'NPC è distante
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag == "ControllableNPC")
            {
                isNear = false;
                refUI.cursorFar.SetActive(true);

            }
            else
            {
                refUI.cursorFar.SetActive(false);
            }
        }
        
        // Se l'NPC è vicino al powerRange
        if (Physics.Raycast(ray, out hit, powerRange))
        {
            refUI.cursorFar.SetActive(false);
            //Prima controlliamo se stiamo mirando ad un personaggio controllabile
            if (hit.collider.tag == "ControllableNPC")
            {
                GetComponent<FSMLogic>().onEnemy = false;
                isNear = true;
                /*
                //Controlliamo se il personaggio mirato abbia il componente Field Of View e in caso lo aggiungiamo
                if (hit.collider.transform.GetComponent<FieldOfView>() == null)
                {
                    // Può buggare l'UI nel caso in cui non ci fosse lo script
                    hit.collider.gameObject.AddComponent<FieldOfView>();
                }*/

                //A questo punto controlliamo se siamo nel Field Of View del personaggio. In caso negativo possiamo usare il potere. //AL MOMENTO ABBIAMO DECISO CHE POSSIAMO USARE I POTERI ANCHE DA DAVANTI
                //if (!hit.collider.gameObject.GetComponent<FieldOfView>().visibleTargets.Contains(this.gameObject.transform))
                //{
                // Logica per effettuare lo switch del corpo
                #region Switch Body Condition
                if (GameManager.Self.ChangePlayerState == GameState.UsePower)
                {


                    GetComponent<FSMLogic>().onEnemy = true;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        spaceListener.Invoke();
                        Camera.main.GetComponentInParent<CameraFilterPack_TV_VHS_Rewind>().enabled = true;
                        GameManager.Self.StopCameraEffect();
                        //Debug.LogWarning("Premi Spazio per entrare");
                        this.gameObject.tag = "ControllableNPC";
                        //this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
                        //this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
                        this.gameObject.transform.GetComponent<CharController>().enabled = false;

                        this.gameObject.transform.GetComponent<FSMLogic>().enabled = false;
                        this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        this.GetComponent<Animator>().SetFloat("Forward", 0);
                        //this.GetComponent<Animator>().SetFloat("Turn", 0);
                        this.gameObject.GetComponent<Animator>().SetLayerWeight(1, 0f);

                        if (this.GetComponent<DialogueHandler>())
                        {
                            this.GetComponent<DialogueHandler>().cantTalk = false;
                        }

                        //Fa tornare il personaggio al suo posto se non è il protagonista
                        if (this.gameObject != GameManager.Self.playerBody)
                        {
                            this.gameObject.GetComponent<State_ControlBody>().returnEvent.Invoke();
                        }
                        // controllo patrolling e isPatrolling è true allora fai partire coroutine
                        if (this.gameObject.GetComponent<Patrolling>() && this.gameObject.GetComponent<Patrolling>().isPatrolling)
                        {
                            this.gameObject.GetComponent<Patrolling>().StartNStopPatrolling(true);
                        }


                        cameraRig.transform.GetComponent<AbstractTargetFollower>().m_Target = null;
                        hit.collider.gameObject.tag = "Player";
                        //hit.collider.transform.GetComponent<ThirdPersonUserControl>().enabled = true;                        
                        //hit.collider.transform.GetComponent<ThirdPersonCharacter>().enabled = true;

                        hit.collider.transform.GetComponent<CharController>().enabled = true;

                        if (hit.collider.transform.GetComponent<DialogueHandler>())
                        {
                            hit.collider.transform.GetComponent<DialogueHandler>().cantTalk = true;
                        }

                        hit.collider.transform.GetComponent<FSMLogic>().enabled = true;
                        hit.collider.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        hit.collider.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                        //Controlliamo se il corpo dove stiamo entrando è quello della protagonista oppure no e in caso attiviamo la UI e il tasto per permettere di tornare nel suo corpo
                        if (hit.collider.gameObject != GameManager.Self.playerBody)
                        {
                            GameManager.Self.outOfYourBody = true;
                            refUI.ReturnUI(true);

                            //Consuma l'energia solo se sta entrando nel corpo di un altro personaggio e non nel suo
                            //GameManager.Self.powerQuantity -= controlBodyCost;
                        }
                        else
                        {
                            refUI.ReturnUI(false);
                            GameManager.Self.outOfYourBody = false;
                        }

                    }
                }
                    #endregion

                    // Logica per effettuare i movimenti in posti precisi agli NPC
                    if (hit.collider.transform.GetComponent<FSM_EnemyPath>() != null)
                    {
                        refUI.PowerUI(true);
                        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Idea") && GameManager.Self.powerQuantity >= mentalPowerCost)
                        {
                            ideaListener.Invoke();
                            //GameManager.Self.powerQuantity -= mentalPowerCost;
                            MoveNPC(hit, 0);
                        }
                    }
                    refUI.cursor.GetComponent<Image>().color = Color.green;
                    

               /* }
                else
                {
                    GetComponent<FSMLogic>().onEnemy = false;
                }*/
            }
            else
            {

                GetComponent<FSMLogic>().onEnemy = false;
                refUI.cursor.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            GetComponent<FSMLogic>().onEnemy = false;
            refUI.cursor.GetComponent<Image>().color = Color.white;
        }
    }

    public void MoveNPC(RaycastHit hitted, int arrayPosition)
    {
        hitted.collider.transform.GetComponent<FSM_EnemyPath>().input = arrayPosition;
        hitted.collider.transform.GetComponent<FSM_EnemyPath>().enabled = true;
    }

    void TooFarFeedback()
    {
        if (!isNear)
        {
            refUI.cursor.GetComponent<Image>().sprite = cursorPoint;
        }
        else
        {
            refUI.cursor.GetComponent<Image>().sprite = cursorFar;
        }
    }

    

	
    

}
