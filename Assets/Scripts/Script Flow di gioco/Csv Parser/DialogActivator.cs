using UnityEngine;
using System.Collections;

public class DialogActivator : MonoBehaviour
{
    public bool isDiagRunning;
    Speaking refSpeak;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastTarget();
        if (refSpeak != null)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                refSpeak.Speak();
                //StartCoroutine(TypeText());
            }
        }
       
    }
    //IEnumerator TypeText()
    //{
    //    isDiagRunning = true;
    //    foreach (char letter in message.ToCharArray())
    //    {
    //        dialogue.text += letter;
    //        yield return new WaitForSeconds(letterPause);
    //    }
    //    yield return new WaitForSeconds(1);
    //    dialogue.text = null;
    //    DisplayedName.text = null;
    //    isDiagRunning = false;

    //}
    public void RaycastTarget()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        refSpeak = null;
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.collider.tag == "ControllableNPC")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.black);
                refSpeak = hit.collider.GetComponent<Speaking>();

            }
        }
    }
}
