using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ReturnInPosition))]


public class EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    
    Vector3 initialPosition;
    public float timeLeft;
    public Transform[] points = new Transform[4];
    public bool isReached = false;
    public bool isOnTime = false;
    int nArray;

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
        isReached = false;
        isOnTime = false;
        GoToPoint(nArray);
    }
	
	// Update is called once per frame
	void Update () {
        
        this.GetComponent<ThirdPersonCharacter>().Move(refNav.desiredVelocity, false, false);
        //Debug.Log(Vector3.Distance(initialPosition, this.transform.position));


        if (GetComponent<NavMeshAgent>().enabled && refNav.remainingDistance < refNav.stoppingDistance && !isOnTime)
        {
           
            StartCoroutine(Timer(timeLeft));
            
        }
        
        if ((Vector3.Distance(initialPosition,this.transform.position)< refNav.stoppingDistance) && isReached)
        {
            isReached = false;


            StartCoroutine(DisableComponents());
            
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
        this.GetComponent<ThirdPersonCharacter>().enabled = true;
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
        GetComponent<ThirdPersonCharacter>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        
        GetComponent<EnemyPath>().enabled = false;
    }

    void OnDisable()
    {
        
        
    }
}
