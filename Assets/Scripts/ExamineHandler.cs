using UnityEngine;
using System.Collections;

public class ExamineHandler : MonoBehaviour
{

    public GameObject anchor;
    public GameObject[] objToExaminate;
    public float maximumDistance = 3;

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
            CheckObject = StartCoroutine(RayMeCO(other));
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
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine(CheckObject);
            playerIn = false;
            anchor = null;
        }
    }

    IEnumerator RayMeCO(Collider player)
    {
        float distance = 100;

        while (playerIn)
        {

            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out hitInfo, maximumDistance, ~(1 << 8)))
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
                        distance = tempDist;
                        anchor = coll.gameObject;
                        foreach (var item in objToExaminate)
                        {
                            if (item != anchor)
                            {
                                item.GetComponent<Examinable>().StopClickMe();
                            }
                        }
                    }
                }

            }
            if (anchor != null)
            {
                anchor.GetComponent<Examinable>().ClickMe();
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
    }
}
