using UnityEngine;
using System.Collections;

public class EnemyPath : MonoBehaviour {

    NavMeshAgent refNav;
    

	// Use this for initialization
	void Start () {
        refNav = GetComponent<NavMeshAgent>();

    }
	
	// Update is called once per frame
	void Update () {
        refNav.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        Animator animPlayer = GetComponent<Animator>();
        animPlayer.SetFloat("Forward", 0.5f);
        
    }
}
