using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExaminableObject : MonoBehaviour {
    [Range(0,60)]
    public float radius = 1f;

    public Sprite memorySprite;

    public List<Transform> hitTargets = new List<Transform>();

    UI refUI;

    public bool isIn = false;
    public Shader outline;
    public Shader nullMaterial;



    void Start ()
    {
        refUI = FindObjectOfType<UI>();
    }
	

	void Update ()
    {
        ChangeMaterial(isIn);
        /*
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
            
        }*/
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            isIn = true;
            StartCoroutine(ClickMe(0.2f));
            
        }
        else
        {
            isIn = false;
        }
    }

    void OnTriggerExit(Collider player)
    {
        
        if (player.tag == "Player")
        {
            isIn = false;
            Debug.LogWarning("sono uscito");
            refUI.ExaminableText(isIn);
            refUI.ExamineMemory(null, false);

        }
        //Debug.LogWarning("Luca gay2");
    }
    
    IEnumerator ClickMe(float delay)
    {
        while (isIn)
        {
            yield return new WaitForSeconds(delay);
            refUI.ExaminableText(isIn);
            if (Input.GetKey(KeyCode.F))
            {
                refUI.ExamineMemory(memorySprite, true);
            }
            else
            {
                refUI.ExamineMemory(null, false);
            }

        }
    }
    /*

    void OnTriggerStay(Collider player)
    {
        if (player.tag == "Player")
        {
            isIn = true;
            refUI.ExaminableText(memorySprite);
        }
        else
        {
            
        }
    }*/
    void ChangeMaterial(bool _isIn)
    {
        if (_isIn)
        {
            
            GetComponent<MeshRenderer>().materials[1].shader = outline;
        }
        else
        {
            GetComponent<MeshRenderer>().materials[1].shader = nullMaterial;

        }
    }
}
