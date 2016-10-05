using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExaminableObject : MonoBehaviour {
    [Range(0,60)]
    public float radius = 5f;

    public List<Transform> hitTargets = new List<Transform>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hitTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

        foreach (var hit in hitColliders)
        {
            if (hit.GetComponent<Collider>().tag == "Player")
            {
                hitTargets.Add(hit.transform);
            }
            
        }
	}



}
