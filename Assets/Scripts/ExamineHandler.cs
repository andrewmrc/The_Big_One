using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExamineHandler : MonoBehaviour
{

    public GameObject anchor;
    public List<GameObject> objToExaminate;
    public float maximumDistanceFromCamera = 3;
    private float rayDistance = 5;
    public GameObject tempAnchor;

    bool playerIn;
    bool drawGizmo = false;
    Coroutine CheckObject;
    Coroutine clickCO;
    Ray ray;
    [Range(0, 5)]
    public float radius = 0.5f;

    RaycastHit hitInfo;
    public Collider[] check;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = true;

            float distance = 100;
            foreach (var obj in objToExaminate)
            {
                float tempDist = Vector3.Distance(other.transform.position, obj.transform.position);
                if (distance > tempDist)
                {
                    distance = tempDist;
                    anchor = obj;
                }
            }
            CheckObject = StartCoroutine(RayMeCO(other));
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CheckObject != null)
            {
                StopCoroutine(CheckObject);
            }

            playerIn = false;
            anchor.GetComponent<Examinable>().StopClickMe();
            anchor = null;
        }
    }

    IEnumerator RayMeCO(Collider player)
    {
        float distance = 100;

        while (playerIn)
        {

            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hitInfo, rayDistance, ~(1 << 8)))
            {

                drawGizmo = true;
                Debug.DrawLine(ray.origin, hitInfo.point);
                check = Physics.OverlapSphere(hitInfo.point, radius, (1 << 9));
                distance = 100;
                foreach (var coll in check)
                {


                    float tempDist = Vector3.Distance(coll.transform.position, hitInfo.point);
                    if (distance > tempDist)
                    {
                        if (anchor != null)
                        {
                            anchor.GetComponent<Examinable>().StopClickMe();
                            tempAnchor = coll.gameObject;
                        }                        
                        
                        
                        if (anchor != null)
                        {
                            
                            float distanceCameraObj = Vector3.Distance(player.transform.position, tempAnchor.transform.position);
                            if (distanceCameraObj < maximumDistanceFromCamera)
                            {
                                anchor = tempAnchor;
                                anchor.GetComponent<Examinable>().ClickMe();
                            }
                            else
                            {
                                anchor.GetComponent<Examinable>().ClickMe();
                            }
                            
                        }
                        distance = tempDist;
                        
                        
                    }
                }

            }
            yield return null;
        }

    }

    void OnDrawGizmos()
    {

        if (drawGizmo)
        {
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawSphere(hitInfo.point, radius);
        }
        else
        {
            Gizmos.color = new Color(0, 1, 0, 0.0f);
        }
        
    }
}
