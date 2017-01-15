﻿using UnityEngine;
using System.Collections;

public class FadeMaterialAlpha : MonoBehaviour {

	public float minRange = 1.5f;
	public float maxRange = 3f;
    Renderer renderer;
    Color color;
    float alpha;


    void Awake()
    {
        renderer = transform.GetComponent<Renderer>();
        color = renderer.material.color;
        alpha = color.a;
    }

	void Update () {
        
        if (gameObject.transform.parent.tag != "Player")
        {
            

            // calcoliamo l'alpha in base alla distanza e ai range settati
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            distance = (distance / 50) * distance;
            //float lerpAmt = 1.0f - Mathf.Clamp01((distance - minRange) / (maxRange - minRange));

            // aggiorniamo l'alpha del materiale

            //color.a = lerpAmt;
            alpha = Mathf.Lerp(1, 0, distance);
            color.a = alpha;
            renderer.material.color = color;




           //  = Color.Lerp(Color.white,Color.clear, distance);
            
        }
		

		//Debug.Log(string.Format("Distance = {0} \t(range = {1} to {2})\t LerpAmt = {3}", distance, minRange, maxRange, lerpAmt));
	}
}
