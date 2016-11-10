using UnityEngine;
using System.Collections;

public class RailCamera : MonoBehaviour {

    public byte i = 0;
    public Transform initialPos;

    public Transform[] cubeArray = new Transform[5];
    //public Transform olivia;

    void Start ()
    {
        StartCoroutine(railCamera());
        initialPos = this.transform;
    }

    void Update ()
    {
            //StartCoroutine(railCamera());
    }

    private IEnumerator railCamera()
    {
        Vector3 currentPos = this.transform.position;
        float distance = 1;

        while (true)
        {

            //this.transform.LookAt(oliviaTarget);
            this.transform.position = Vector3.Lerp(this.transform.position, cubeArray[i].position, Time.deltaTime * 3f);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, cubeArray[i].transform.rotation, Time.deltaTime * 2);


            if (Input.GetKeyDown(KeyCode.E))
            {
                yield break;
            }

            if ((this.transform.position - cubeArray[i].position).magnitude > distance)
            {
                yield return null;
            }

            else
            {
                i++;
                //currentPos = this.transform.position;
                distance = 0.5f;

                yield return null;

                /*if (i == cubeArray.Length-1)
                {
                    Debug.Log("i'm in");
                    this.transform.position = Vector3.Lerp(this.transform.position, cubeArray[i].position, Time.deltaTime * 4f);

                    if ((this.transform.position - cubeArray[i].position).magnitude > 0.01f)
                    {
                        yield return null;
                    }
                    //i++;
                }*/

                if (i >= 5)
                {
                    distance = 0.01f;
                    Debug.Log(distance);
                    this.transform.position = initialPos.position;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, initialPos.rotation, Time.deltaTime * 2);
                    //this.gameObject.SetActive(false);
                    //yield return new WaitForSeconds(1f);
                    yield break;
                }
            }
       }
        this.transform.position = initialPos.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, initialPos.rotation, Time.deltaTime * 2);

    }
}
