using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;


public class ReturnInPosition : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
	public float waitTime;

    Vector3 initialPosition;

    void Awake()
    {
        refNav = GetComponent<NavMeshAgent>();
        GetComponent<ReturnInPosition>().enabled = false;
        initialPosition = this.transform.position;
		PowerController bodyControlHandle = GetComponent<PowerController>();
        bodyControlHandle.returnEvent.AddListener(MyPosition);
    }


    // Use this for initialization
    void Start () {
         
    }
	
	// Update is called once per frame
	void Update () {
        if (refNav.remainingDistance > refNav.stoppingDistance)
        {
            GetComponent<ThirdPersonCharacter>().Move(refNav.desiredVelocity, false, false);
        }
        else
        {
            GetComponent<ThirdPersonCharacter>().Move(Vector3.zero, false, false);
            StartCoroutine(DisableComponents());
            
        }
        
    }


    public void MyPosition()
    {
		StartCoroutine(BackToMyPlace());
    }


	//aggiungiamo un delay prima di rimandare il personaggio al suo posto
	IEnumerator BackToMyPlace () {
		yield return new WaitForSeconds (waitTime);
		refNav.enabled = true;
		this.GetComponent<ThirdPersonCharacter>().enabled = true;
		Debug.LogWarning(Vector3.Distance(initialPosition, this.transform.position) > refNav.stoppingDistance);
		if (Vector3.Distance (initialPosition, this.transform.position) > refNav.stoppingDistance) {
			Debug.LogWarning ("Sono distante");

			GetComponent<ReturnInPosition> ().enabled = true;
			refNav.destination = initialPosition;


		} else {
			StartCoroutine(DisableComponents());
		}
	}


    IEnumerator DisableComponents()
    {
		yield return new WaitForSeconds(0.5f);
        GetComponent<ReturnInPosition>().enabled = false;
		GetComponent<NavMeshAgent>().enabled = false;
		this.GetComponent<ThirdPersonCharacter>().enabled = false;
		this.GetComponent<ThirdPersonUserControl>().enabled = false;

		this.gameObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    }
}
