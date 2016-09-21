using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    GameManager refGM;
    public Transform position_one;

	// Use this for initialization
	void Start () {
        refNav = GetComponent<NavMeshAgent>();
        refGM = FindObjectOfType<GameManager>();


    }
	
	// Update is called once per frame
	void Update () {

        //if (GameManager.Self.UI_Power.active == true)
        {
            refNav.destination = position_one.position;

            Animator animPlayer = GetComponent<Animator>();
            animPlayer.SetFloat("Forward", 0.6f);

            
            
            if (refNav.remainingDistance < 0.05f)
            {
                
                animPlayer.SetFloat("Forward", 0);
                

            }
        }

        
        
    }
}
