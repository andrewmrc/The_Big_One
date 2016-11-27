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

    //Variabili Patroling
    public PatrollingDB[] moveTransform;
    public GameObject patrolingObj;
    public float speedObj;
    public float stunWaiting;

    // Variabili utilizzate per l'esecuzione di N volte
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
    void StartPatroling()
    {
        patrolingObj.GetComponent<Patrolling>().patrollingTransform = moveTransform;
        patrolingObj.GetComponent<Patrolling>().obj = patrolingObj;
        patrolingObj.GetComponent<Patrolling>().delayStartPatrol = stunWaiting;
        patrolingObj.GetComponent<Patrolling>().StartNStopPatrolling(true);
    }


    IEnumerator SpawnerRoutine()
    {
        //Instantiate (objectToUse).transform.position = new Vector3(positionToSpawn.position.x, positionToSpawn.position.y, positionToSpawn.position.z);
        //objectToUse.GetComponent<CharController> ().enabled = true;
        yield return new WaitForSeconds(2f);
        //objectToUse.GetComponent<CharController> ().enabled = false;
        Debug.Log("SPAWNA");
    }

    IEnumerator PatroingCO()
    {
        NavMeshAgent objNav = patrolingObj.GetComponent<NavMeshAgent>();
        
        //objNav.enabled = true;
        int k = 0;
        /*while (true)
        {
            


            while (Vector3.Distance(patrolingObj.transform.position, patrolingTrans[k].position) > 0.6f)
            {
                if (!patrolingObj.GetComponent<FSMLogic>().enabled)
                {
                    objNav.destination = patrolingTrans[k].position;
                    
                    objNav.GetComponent<FSM_ReturnInPosition>().isWaiting = false;
                }
                else
                {
                    objNav.enabled = false;
                }

                
                yield return null;
            }
            k++;
            if (k >= patrolingTrans.Length)
            {
                k = 0;
            }

            yield return new WaitForSeconds(delay);
            
        }*/
        yield return null;

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
                case Condition.Patroling:
                    StartPatroling();
                    break;
                default:
                    break;
            }
        }
    }
}
