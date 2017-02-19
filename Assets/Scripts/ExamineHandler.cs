using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineHandler : MonoBehaviour
{
    public GameObject anchor;

    public float distanceFromPlayer = 2;

    //[HideInInspector]
    //public List<GameObject> objToExaminate;

    [Range(0, 5)]
    public float radius = 0.5f;
	public bool gizmoDraw = false;

    private Collider[] check;
    private Coroutine CheckObject;
    private bool drawGizmo = false;
    private RaycastHit hitInfo;
    private bool playerIn;
    private Ray ray;
    private float rayDistance = 4;
    private GameObject tempAnchor;

    private void OnTriggerStay(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			if (!playerIn) {
				playerIn = true;
				Debug.Log ("Dentro Trigger Esaminabile");
				CheckObject = StartCoroutine (RayMeCO (other));
			}
		} else {
			playerIn = false;
		}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CheckObject != null)
            {
                StopCoroutine(CheckObject);
            }
            playerIn = false;
            if (anchor != null)
            {
                anchor.GetComponent<Examinable>().StopClickMe();
            }
            anchor = null;
        }
    }

    private IEnumerator RayMeCO(Collider player)
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
							//Debug.Log ("asd");
                        }
                        tempAnchor = coll.gameObject;

                        //Vector3 headPlayerVector = new Vector3(player.transform.position.x, 1, player.transform.position.z);
						float distanceCameraObj = Vector3.Distance(player.transform.position, tempAnchor.transform.position);

						//Debug.Log ("distanceCameraObj:" + distanceCameraObj);

                        if (distanceCameraObj < distanceFromPlayer)
                        {
                            anchor = tempAnchor;
                        }

                        distance = tempDist;
                    }
                }
                if (anchor != null)
                {
                    anchor.GetComponent<Examinable>().ClickMe();
                }
            }
            else
            {
                if (anchor != null)
                {
                    anchor.GetComponent<Examinable>().StopClickMe();
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (gizmoDraw)
        {
            if (drawGizmo)
            {
                Gizmos.color = new Color(0, 1, 0, 0.1f);
                Gizmos.DrawSphere(hitInfo.point, radius);
            }
        }
    }
}