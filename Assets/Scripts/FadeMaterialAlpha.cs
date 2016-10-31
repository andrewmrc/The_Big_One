using UnityEngine;
using System.Collections;

public class FadeMaterialAlpha : MonoBehaviour {

	public float minRange = 1.5f;
	public float maxRange = 3f;

	void Update () {

		// calcoliamo l'alpha in base alla distanza e ai range settati
		float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		float lerpAmt = 1.0f - Mathf.Clamp01((distance - minRange) / (maxRange - minRange));

		// aggiorniamo l'alpha del materiale
		Renderer renderer = transform.GetComponent<Renderer>();
		Color color = renderer.material.color;
		color.a = lerpAmt;
		renderer.material.color = color;

		//Debug.Log(string.Format("Distance = {0} \t(range = {1} to {2})\t LerpAmt = {3}", distance, minRange, maxRange, lerpAmt));
	}
}
