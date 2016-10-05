using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExaminableObject : MonoBehaviour {
    [Range(0,60)]
    public float radius = 5f;

    public Sprite memorySprite;

    public List<Transform> hitTargets = new List<Transform>();

    UI refUI;

    // Use this for initialization
    void Start () {
        refUI = FindObjectOfType<UI>();
    }
	
	// Update is called once per frame
	void Update () {
        
        hitTargets.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

        foreach (var hit in hitColliders)
        {
            if (hit.GetComponent<Collider>().tag == "Player")
            {
                refUI.ExaminableText(memorySprite, true);
                 
            }
            else
            {
                refUI.ExaminableText(memorySprite, false);
            }
            
        }
	}



}
