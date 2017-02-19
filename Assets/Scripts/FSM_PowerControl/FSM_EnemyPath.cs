using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Events;

//[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(FSM_ReturnInPosition))]


public class FSM_EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    
    Vector3 initialPosition;
    public float timeLeft = 5f;
    public Transform[] points = new Transform[4];
    public bool isReached = false;
    public bool isOnTime = false;
    int nArray;
	public bool executed;
    public UnityEvent destinationEvent;
	public UnityEvent returnEvent;

    public int input { set { nArray = value; } }

    

    // Use this for initialization
    void Awake () {
        refNav = GetComponent<NavMeshAgent>();
        refGM = FindObjectOfType<GameManager>();
        initialPosition = this.transform.position;
    }

    void OnEnable()
    {
		GetComponent<FSM_ReturnInPosition>().isWaiting = false;
        isReached = false;
        isOnTime = false;
        GoToPoint(nArray);
    }
	
	// Update is called once per frame
	void Update () {
        
		this.GetComponent<CharController>().Move(refNav.desiredVelocity, false);
        //Debug.Log(Vector3.Distance(initialPosition, this.transform.position));


        if (GetComponent<NavMeshAgent>().enabled && refNav.remainingDistance < refNav.stoppingDistance && !isOnTime)
        {
			if (!executed) {
				executed = true;
				destinationEvent.Invoke ();
			}
            StartCoroutine(Timer(timeLeft));
            
        }
        
        if ((Vector3.Distance(initialPosition,this.transform.position)< refNav.stoppingDistance) && isReached)
        {
            isReached = false;

			returnEvent.Invoke (); //chiama l'evento quando si torna alla posizione di partenza

            StartCoroutine(DisableComponents());
            
        }

        if (GetComponent<FSMLogic>().enabled)
        {
            refNav.enabled = false;
            GetComponent<FSM_EnemyPath>().enabled = false;
        }
    }

    void ResetPosition()
    {
        
        if (refNav.remainingDistance < refNav.stoppingDistance)
        {
            refNav.destination = initialPosition;
            isReached = true;
        }
    }

    void GoToPoint(int _input)
    {
        refNav.enabled = true;        
        refNav.destination = points[nArray].position;
		this.GetComponent<CharController>().enabled = true;
    }

    IEnumerator Timer(float seconds)
    {
        isOnTime = true;
        yield return new WaitForSeconds(seconds);
        ResetPosition();
        
    }
    IEnumerator DisableComponents()
    {
        refNav.Stop();
        GetComponent<NavMeshAgent>().enabled = false;
		GetComponent<CharController>().enabled = false;
		this.transform.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(0.5f);
        
        
        GetComponent<FSM_EnemyPath>().enabled = false;
    }

    void OnDisable()
    {
		GetComponent<FSM_ReturnInPosition>().isWaiting = true;
        
    }
}
