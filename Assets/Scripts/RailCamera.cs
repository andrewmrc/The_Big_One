using UnityEngine;
using System.Collections;

public class RailCamera : MonoBehaviour {

    public byte i = 0;

    public Transform[] cubeArray = new Transform[5];
    //public Transform olivia;

    void Start ()
    {
        StartCoroutine(railCamera());


    }

    void Update ()
    {
            //StartCoroutine(railCamera());
    }

    private IEnumerator railCamera()
    {
        Vector3 currentPos = this.transform.position;

        while (true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, cubeArray[i].position, Time.deltaTime * 1.5f);
            if (Input.GetKeyDown(KeyCode.E))
            {
                yield break;
            }

            if ((currentPos - cubeArray[i].position).magnitude > 2f)
            {
                yield return null;
            }
            else
            {
                i++;
                currentPos = this.transform.position;
                yield return null;
            }
        }
    }
}
