using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExaminableObject : MonoBehaviour {


    public Sprite memorySprite;



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
            StartCoroutine(ClickMe(0.1f));
            ChangeMaterial(isIn);

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
            ChangeMaterial(isIn);

        }
       
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
