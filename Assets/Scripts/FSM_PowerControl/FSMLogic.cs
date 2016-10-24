using UnityEngine;
using System.Collections;

public class FSMLogic : MonoBehaviour {

    public float powerRange;
    public bool isShowMemory = false;
    public bool isAiming = false;



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
                if (hit.collider.GetComponent<State_ShowMemory>())
                {
                    refUI.MemoryUI(true);
                    if (Input.GetKeyDown(KeyCode.F) && !isShowMemory)
                    {
                        sm.HandleInput(InputTransition.ShowMemory);
                        isShowMemory = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.F) && isShowMemory)
                    {
                        sm.HandleInput(InputTransition.UnshowMemory);
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
