using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;

public class FSMLogic : MonoBehaviour {

    public float powerRange = Mathf.Infinity;
    public bool isShowMemory = false;
    public bool isAiming = false;

    public Sprite imageSprite;


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
        
        if (Input.GetKey(KeyCode.Mouse1) && !isShowMemory)
        {
            RaycastHandle();
            sm.HandleInput(InputTransition.MouseButtonOneDown);
            
            isAiming = true;
            
        }
        else
        {
            sm.HandleInput(InputTransition.MouseButtonOneUp);
            isAiming = false;
            onEnemy = false;
            UIActivator();

        }
        if (!isAiming && isShowMemory && Input.GetKeyDown(KeyCode.F))
        {
            sm.HandleInput(InputTransition.UnshowMemory);
            isShowMemory = false;
            this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
            this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
            Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
        }

        

        sm.StateUpdate();


	}


    public void RaycastHandle()
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
                    if (Input.GetKeyDown(KeyCode.F) && !isShowMemory)
                    {
                        sm.HandleInput(InputTransition.ShowMemory);
                        imageSprite = hit.collider.GetComponent<MemoryContainer>().memoryImage;
                        isShowMemory = true;
                        this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = false;
                        this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = false;
                        Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.F) && isShowMemory)
                    {
                        sm.HandleInput(InputTransition.UnshowMemory);
                        this.gameObject.transform.GetComponent<ThirdPersonUserControl>().enabled = true;
                        this.gameObject.transform.GetComponent<ThirdPersonCharacter>().enabled = true;
                        Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
                        isShowMemory = false;
                        
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
            refUI.HackUI(false);
            refUI.MemoryImageUIHand(false);

        }
    }
}
