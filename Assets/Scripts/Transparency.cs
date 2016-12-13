using UnityEngine;
using System.Collections;

public class Transparency : MonoBehaviour {

    RaycastHit oldHit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        XRay();
    }

    // Hacer a los objetos que interfieran con la vision transparentes
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
                Color colorA = oldHit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
                colorA.a = 1f;
                oldHit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("_Color", colorA);
            }

            // Add transparence
            Color colorB = hit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
            colorB.a = 0.5f;
            hit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.SetColor("_Color", colorB);

            // Save hit
            oldHit = hit;
        }
    }
}