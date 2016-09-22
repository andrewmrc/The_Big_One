using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    public Transform position_one;
    public Vector3 position_zero;
    private float timeLeft;

    // Use this for initialization
    void Awake () {
        
        refNav = GetComponent<NavMeshAgent>();
        refGM = FindObjectOfType<GameManager>();
        position_zero = this.transform.position;
    }

    void OnEnable()
    {
        timeLeft = 3.0f;
        refNav.destination = position_one.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        //if (GameManager.Self.UI_Power.active == true)
        {
            print(refNav.pathEndPosition);
            Animator animPlayer = GetComponent<Animator>();
            animPlayer.SetFloat("Forward", 0.6f);           
            
            if (refNav.remainingDistance < 0.01f)
            {
                timeLeft -= Time.deltaTime;                
                animPlayer.SetFloat("Forward", 0);
               
            }
            if (timeLeft < 0)
            {
                refNav.destination = position_zero;
                animPlayer.SetFloat("Forward", 0.6f);
                if (refNav.remainingDistance < 0.01f && timeLeft < -0.1f)
                {
                    animPlayer.SetFloat("Forward", 0);
                    this.GetComponent<EnemyPath>().enabled = false;
                    
                }
            }
        }

        
        
    }
}
