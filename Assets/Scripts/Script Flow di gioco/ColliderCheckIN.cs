using UnityEngine;
using System.Collections;

public class ColliderCheckIN : MonoBehaviour
{

    public GameObject target;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {
            gameObject.GetComponent<MeshCollider>().isTrigger = true;
            target.AddComponent<Badge>();
        }
    }
    void OnTriggerExit(Collider col)
    {
        gameObject.GetComponent<MeshCollider>().isTrigger = false;
    }
}
