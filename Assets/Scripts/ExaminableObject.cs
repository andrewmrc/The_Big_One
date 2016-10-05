using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExaminableObject : MonoBehaviour {
    [Range(0,60)]
    public float radius = 1f;

    public Sprite memorySprite;

    public List<Transform> hitTargets = new List<Transform>();

    UI refUI;

    void Start ()
    {
        refUI = FindObjectOfType<UI>();
    }
	

	void Update ()
    {
        
        hitTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

        foreach (var hit in hitColliders)
        {
            if (hit.GetComponent<Collider>().tag == "Player")
            {
                refUI.ExaminableText(memorySprite);
                 
            }
            else
            {
                // cercare un modo di disattivare ExaminableText in UI
            }
            
        }
	}



}
