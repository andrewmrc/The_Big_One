using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    public Transform position_one;
    Vector3 initialPosition;
    private float timeLeft;
    public Transform[] points = new Transform[4];
    bool ciao;
    
    public int input;

    // Use this for initialization
    void Awake () {
        
        refNav = GetComponent<NavMeshAgent>();
        refGM = FindObjectOfType<GameManager>();
        initialPosition = this.transform.position;
        
    }

    void OnEnable()
    {
        timeLeft = 3.0f;
        GoToPoint(input);
        ciao = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (refNav.remainingDistance < 0.5f)
        {
            if (!ciao)
            {
                timeLeft = Time.time;
                ciao = true;
            }
            
            if (Time.time - timeLeft > 3)
            {
                ResetPosition();
            }            
        }
    }

    void ResetPosition()
    {
        refNav.destination = initialPosition;
        if (refNav.remainingDistance < 0.5f)
        {
            GetComponent<EnemyPath>().enabled = false;
        }
    }

    void GoToPoint(int _input)
    {

        refNav.destination = points[input].position;

    }
}
