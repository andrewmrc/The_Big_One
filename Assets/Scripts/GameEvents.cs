
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/*
[Serializable]
public class SpawnEvent : UnityEvent <GameObject, Transform> {}*/

public class GameEvents : MonoBehaviour
{

    public enum Condition { Spawner, PlayAnimationFloat, PlayAnimationBool, LoadScene,
                            RandomActionSequence,ActionSequence, Patrolling, ShowText, UnityEventsActivator}
    public Condition whichEvent;

    public GameObject objectToUse;
    public Transform positionToSpawn;
    public string animationName;
    public float animationValueFloat;
    public bool animationValueBool;
    public string sequenceName;
    public int positionArray;

    // Variabili per compattare
    public bool isSequenceRandom;
    public bool isSequence;
    public int choice;

    //Variabili Patroling
    public PatrollingDB[] moveTransform;
    public GameObject patrolingObj;
    public float speedObj;
    public float stunWaiting;

    // Variabili utilizzate per l'esecuzione di N volte
    public int n;
    public int i = 0;
    
	//public SpawnEvent spawner;

	//Nome scena da caricare
	public string sceneName;

	//Variabili per lo Show Text
	public string[] textToShowList;


	//Variabili per l'evento player
	public GameObject character;
	public bool executed;
	public UnityEvent eventToActivate;


	//Variabili per l'array di UnityEvents con delay
	public UnityEvent[] unityEventsList;
	public float delayBetweenEvents = 0.5f;
    
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


	void LoadScene()
	{
		Fader refFader;
		refFader = FindObjectOfType<Fader>();
		refFader.StartCoroutine(refFader.FadeIn());
		StartCoroutine (LoadLevelCo (sceneName));
		//SceneManager.LoadScene (sceneName);
	}


	IEnumerator LoadLevelCo (string levelName) {
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene (levelName);
	}


    void RandomActionSequence()
    {
        FlowManager.Self.ExecuteRandomEvent(sequenceName, positionArray);
    }
    

	void ActionSequence()
    {
        FlowManager.Self.ExecuteNewSequenceEvent(choice,positionArray);
    }


    void StartPatroling()
    {
        patrolingObj.GetComponent<Patrolling>().patrollingTransform = moveTransform;
        patrolingObj.GetComponent<Patrolling>().obj = patrolingObj;
        patrolingObj.GetComponent<Patrolling>().delayStartPatrol = stunWaiting;
        patrolingObj.GetComponent<Patrolling>().StartNStopPatrolling(true);
    }


	void ShowText (){
		StartCoroutine(ShowTextCo());
	}

	IEnumerator ShowTextCo()
	{
		for (int i = 0; i < textToShowList.Length; i++) {
			GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = textToShowList [i];
			yield return new WaitForSeconds(5f);
		}
		GameManager.Self.canvasUI.GetComponent<UI> ().VariousDescriptionUI.GetComponent<Text> ().text = "";
	}



	void EventsActivator (){
		if (!executed) {
			executed = true;
			StartCoroutine(EventActivatorCo());
		}
	}


	IEnumerator EventActivatorCo()
	{
		for (int i = 0; i < unityEventsList.Length; i++) {
			//yield return new WaitForSeconds(delayBetweenEvents);
			unityEventsList [i].Invoke ();
			yield return new WaitForSeconds(delayBetweenEvents);
		}
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
            //i++;
            switch (whichEvent)
            {

                case Condition.PlayAnimationBool:
                    PlayAnimationBool();
                    ExecuteRandomOrSequence();
                    break;
                case Condition.PlayAnimationFloat:
                    PlayAnimationFloat();
                    ExecuteRandomOrSequence();
                    break;
                case Condition.Spawner:
                    Spawner();
                    ExecuteRandomOrSequence();
                    break;
                case Condition.LoadScene:
					LoadScene();
                    break;
                case Condition.RandomActionSequence:
                    RandomActionSequence();
                    break;
                case Condition.ActionSequence:
                    i = 0;
                    ActionSequence();
                    break;
                case Condition.Patrolling:
                    StartPatroling();
                    ExecuteRandomOrSequence();
                    break;
				case Condition.ShowText:
					ShowText();
					break;
				case Condition.UnityEventsActivator:
					EventsActivator();
					break;
                default:
                    break;
            }
        }
    }

    void ExecuteRandomOrSequence()
    {
        if (isSequenceRandom)
        {
            FlowManager.Self.ExecuteNewRandomEvent(choice);
        }
        if (isSequence)
        {
            FlowManager.Self.ExecuteNewSequenceEvent(choice, positionArray);
        }
    }
}
