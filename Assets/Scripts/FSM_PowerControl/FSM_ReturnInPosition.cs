using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;


public class FSM_ReturnInPosition : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
	public float waitTime;

    Vector3 initialPosition;

    public bool isWaiting = true;

    void Awake()
    {
        refNav = GetComponent<NavMeshAgent>();
        GetComponent<FSM_ReturnInPosition>().enabled = false;
        initialPosition = this.transform.position;
		State_ControlBody bodyControlHandle = GetComponent<State_ControlBody>();
        bodyControlHandle.returnEvent.AddListener(MyPosition);
    }


    // Use this for initialization
    void Start () {
         
    }
	
	// Update is called once per frame
	void Update () {

		/*
        if (!GetComponent<FSMLogic>().enabled)
        {
			Debug.Log ("remaining: " + refNav.remainingDistance);
			Debug.Log (refNav.remainingDistance > refNav.stoppingDistance);
            if (refNav.remainingDistance > refNav.stoppingDistance)
            {
                //GetComponent<ThirdPersonCharacter>().Move(refNav.desiredVelocity, false, false);
				GetComponent<CharController>().Move(refNav.desiredVelocity, false);
				Debug.Log ("remaaaaa: " + refNav.remainingDistance);
            }
            else
            {
                //GetComponent<ThirdPersonCharacter>().Move(Vector3.zero, false, false);
				GetComponent<CharController>().Move(refNav.desiredVelocity, false);
				Debug.Log ("ciaooooo: " + refNav.remainingDistance);
                StartCoroutine(DisableComponents());

            }
        }*/

        if (GetComponent<FSMLogic>().enabled)
        {
			isWaiting = false;
            refNav.enabled = false;
            GetComponent<FSM_ReturnInPosition>().enabled = false;

        }
         
    }


    public void MyPosition()
    {
        if (GetComponent<Patrolling>() && !GetComponent<Patrolling>().isPatrolling)
        {
            StartCoroutine(BackToMyPlace());
        }
		
    }


	//aggiungiamo un delay prima di rimandare il personaggio al suo posto
	IEnumerator BackToMyPlace () {
		yield return new WaitForSeconds (waitTime);
		isWaiting = true;
		while (isWaiting)
        {
            refNav.enabled = true;
            //this.GetComponent<ThirdPersonCharacter>().enabled = true;
			this.GetComponent<CharController>().enabled = true;

            if (Vector3.Distance(initialPosition, this.transform.position) > refNav.stoppingDistance)
            {
                GetComponent<FSM_ReturnInPosition>().enabled = true;
                refNav.destination = initialPosition;
				GetComponent<CharController>().Move(refNav.desiredVelocity, false);
            }
            else
            {
				isWaiting = false;
                StartCoroutine(DisableComponents());
            }
			yield return null;
        }
		
	}


    IEnumerator DisableComponents()
    {
		Debug.Log ("CHIAMATA STRANA");
		yield return new WaitForSeconds(0.5f);
        GetComponent<FSM_ReturnInPosition>().enabled = false;
		GetComponent<NavMeshAgent>().enabled = false;
		//this.GetComponent<ThirdPersonCharacter>().enabled = false;
		//this.GetComponent<ThirdPersonUserControl>().enabled = false;
		this.GetComponent<CharController>().enabled = false;

		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }
}
