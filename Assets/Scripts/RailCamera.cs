using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailCamera : MonoBehaviour {

    private byte i = 0;
    public float distance = 1f;
    public float waitTime = 1f;
    public float speed = 3f;
    public GameObject fade;
    public List<Transform> cubeList = new List<Transform>(5);
    public List<Transform> listTarget = new List<Transform>();

    void Start ()
    {
        StartCoroutine(railCamera());
    }

    private IEnumerator railCamera()
    {
        Vector3 currentPos = this.transform.position;

        while (true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, cubeList[i].position, Time.deltaTime * speed);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeList[i].transform.rotation, Time.deltaTime * speed);

            if (Input.GetKeyDown(KeyCode.E))
            {
                yield break;
            }

            if ((this.transform.position - cubeList[i].position).magnitude > distance)
            {
                //this.transform.LookAt(listTarget[i]);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
                i++;
                distance = 0.5f;

                yield return null;

                if (i >= 5)
                {
                    FadeIn();
                    this.gameObject.SetActive(false);
                    
                    yield return new WaitForSeconds(3);
                    yield break;
                }
            }
       }
    }

    public void FadeIn()
    {
        fade.GetComponent<CanvasGroup>().alpha += 0.5f;
    }
}
