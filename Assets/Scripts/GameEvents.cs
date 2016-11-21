using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

/*
[Serializable]
public class SpawnEvent : UnityEvent <GameObject, Transform> {}*/

public class GameEvents : MonoBehaviour
{


    public enum Condition { Spawner, PlayAnimationFloat, PlayAnimationBool, LoadScene,
                            RandomActionSequence,ActionSequence, Patroling }
    public Condition whichEvent;

    public GameObject objectToUse;
    public Transform positionToSpawn;
    public string animationName;
    public float animationValueFloat;
    public bool animationValueBool;
    public string sequenceName;
    public int positionArray;

    public int n;
    public int i = 0;
    //public SpawnEvent spawner;

    void Spawner()
    {
        Instantiate(objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
        //StartCoroutine (SpawnerRoutine ());
    }

    void PlayAnimationFloat()
    {
        objectToUse.GetComponent<Animator>().SetFloat(animationName, animationValueFloat);
        Debug.Log("PLAYANIMFLOAT" + animationName + animationValueFloat);
    }

    void PlayAnimationBool()
    {
        objectToUse.GetComponent<Animator>().SetBool(animationName, animationValueBool);
        Debug.Log("PLAYANIMBOOL");
    }

    void RandomActionSequence()
    {
        FlowManager.Self.ExecuteRandomEvent(sequenceName, positionArray);
    }
    void ActionSequence()
    {
        FlowManager.Self.ExecuteSequenceEvent(sequenceName,positionArray);
    }


    IEnumerator SpawnerRoutine()
    {
        //Instantiate (objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
        //objectToUse.GetComponent<CharController> ().enabled = true;
        yield return new WaitForSeconds(2f);
        //objectToUse.GetComponent<CharController> ().enabled = false;
        Debug.Log("SPAWNA");
    }




    public void ExecuteNTimes(int n)
    {

        if (i < n)
        {
            i++;
            switch (whichEvent)
            {

                case Condition.PlayAnimationBool:
                    PlayAnimationBool();
                    break;
                case Condition.PlayAnimationFloat:
                    PlayAnimationFloat();
                    break;
                case Condition.Spawner:
                    Spawner();
                    break;
                case Condition.LoadScene:
                    Spawner();
                    break;
                case Condition.RandomActionSequence:
                    RandomActionSequence();
                    break;
                case Condition.ActionSequence:
                    i = 0;
                    ActionSequence();
                    break;
                default:
                    break;
            }
        }
    }
}
