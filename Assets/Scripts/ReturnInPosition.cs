using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;


public class ReturnInPosition : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    

    Vector3 initialPosition;

    void Awake()
    {
        refNav = GetComponent<NavMeshAgent>();
        GetComponent<ReturnInPosition>().enabled = false;
        initialPosition = this.transform.position;
        BodyControlPower bodyControlHandle = GetComponent<BodyControlPower>();
        bodyControlHandle.returnEvent.AddListener(MyPosition);
    }
    // Use this for initialization
    void Start () {
         
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<ThirdPersonCharacter>().Move(refNav.desiredVelocity, false, false);
        if ((Vector3.Distance(initialPosition, this.transform.position) > refNav.stoppingDistance))
        {
            
        }
    }

    void OnEnable()
    {
        
        
    }

    public void MyPosition()
    {
        refNav.enabled = true;

        Debug.LogWarning(Vector3.Distance(initialPosition, this.transform.position) > refNav.stoppingDistance);
        if (Vector3.Distance(initialPosition, this.transform.position) > refNav.stoppingDistance)
        {
            Debug.LogWarning("Sono distante");
            GetComponent<ReturnInPosition>().enabled = true;
            refNav.destination = initialPosition;
            
        }
    }
}
