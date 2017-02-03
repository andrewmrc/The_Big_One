using UnityEngine;
using System.Collections;

public class RotationAllarmeMeno90 : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime);
    }
}

