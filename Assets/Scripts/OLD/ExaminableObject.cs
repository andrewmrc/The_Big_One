using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;



public class ExaminableObject : MonoBehaviour {


    public Sprite memorySprite;

    bool isLooking = false;
    bool isClicked = false;

    UI refUI;

    public bool isIn = false;
    Shader outline;
    Shader nullMaterial;

    public string text;



    void Start ()
    {
        refUI = FindObjectOfType<UI>();
        outline = Resources.Load("Outline") as Shader;
        nullMaterial = Resources.Load("Null") as Shader;
    }
	


    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            isIn = true;
            StartCoroutine(ClickMe(0));
            ChangeMaterial(isIn);

        }       
        
    }

    void OnTriggerExit(Collider player)
    {
        
        if (player.tag == "Player")
        {
            refUI.ExaminableText(false);
            isIn = false;
            refUI.ExamineMemory(null, false);
            ChangeMaterial(isIn);
            

        }
        

    }
    

    IEnumerator ClickMe(float delay)
    {
        
        RaycastHit hit;
        while (isIn)
        {
            isLooking = false;
            yield return new WaitForSeconds(delay);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hit, 5f))
            {
                
                
                Debug.DrawLine(ray.origin, hit.point, Color.magenta);
                if (hit.collider.GetComponent<ExaminableObject>() && !isClicked)
                {
                    refUI.ExamineMemory(null, false);
                    isLooking = true;
                    
					if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine"))
                    {
                        isClicked = true;
                        isLooking = false;
                        refUI.ExamineMemory(memorySprite, true);
                        refUI.ExaminableText(isLooking);
                        refUI.TextToShow(text, true);
                        Camera.main.GetComponentInParent<FreeLookCam>().enabled = false;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>().enabled = false;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().enabled = false;

                        GameObject.FindGameObjectWithTag("Player").GetComponent<PowerController>().enabled = false;
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Forward", 0);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetFloat("Turn", 0);
                    }
                   
                    
                }
				else if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown ("Examine") && isClicked)
                {
                    isClicked = false;
                    isLooking = true;
                    refUI.ExamineMemory(memorySprite, false);
                    refUI.TextToShow(text, false);
                    Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>().enabled = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().enabled = true;

                    GameObject.FindGameObjectWithTag("Player").GetComponent<PowerController>().enabled = true;
                }
                refUI.ExaminableText(isLooking);

            }


        }
        refUI.ExaminableText(false);
        Camera.main.GetComponentInParent<FreeLookCam>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonUserControl>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PowerController>().enabled = true;
    }

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
