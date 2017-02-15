using UnityEngine;
using System.Collections;

public class FadeMaterialAlpha : MonoBehaviour {

	public float minRange = 1.5f;
	public float maxRange = 3f;
    Renderer renderer;
    Color color;
    float alpha;
	bool animated = true;

    void Awake()
    {
        renderer = transform.GetComponent<Renderer>();
        color = renderer.material.color;
        alpha = color.a;
    }

	void Update () {
        
		if (gameObject.transform.root.tag != "Player")
        {
            

            // calcoliamo l'alpha in base alla distanza e ai range settati
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            //distance = (distance / 50) * distance;
            float lerpAmt = 1.0f - Mathf.Clamp01((distance - minRange) / (maxRange - minRange));

            // aggiorniamo l'alpha del materiale

            color.a = lerpAmt;
            //alpha = Mathf.Lerp(1, 0, distance);
            //color.a = alpha;
			renderer.material.color = color;
			if (lerpAmt > .01f && !animated) {
				if (GetComponentInChildren<Animator> ()) {
					GetComponentInChildren<Animator> ().Play (0);
					animated = true;
				}
			}				

			if (lerpAmt <= 0) {
				animated = false;
			}
           //  = Color.Lerp(Color.white,Color.clear, distance);

        }
        else
        {
            renderer.material.color = Color.clear;
        }
		

		//Debug.Log(string.Format("Distance = {0} \t(range = {1} to {2})\t LerpAmt = {3}", distance, minRange, maxRange, lerpAmt));
	}
}
