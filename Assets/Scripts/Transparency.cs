using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

    RaycastHit oldHit;

    void Update()
    {
        XRay();
    }

    private void XRay()
    {

        float characterDistance = Vector3.Distance(transform.position, GameObject.Find("Character").transform.position);
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, characterDistance))
        {
            if (oldHit.transform)
            {

                // Add transparence
                
                Color colorA = oldHit.transform.gameObject.GetComponentInChildren<MeshRenderer>().material.color;
                colorA.a = 1f;
                oldHit.transform.gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", colorA);
            }

            // Add transparence
            Color colorB = hit.transform.gameObject.GetComponentInChildren<MeshRenderer>().material.color;
            colorB.a = 0.5f;
            hit.transform.gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", colorB);

            // Save hit
            oldHit = hit;
        }
    }
}