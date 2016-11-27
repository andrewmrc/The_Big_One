using UnityEngine;
using System.Collections;

[System.Serializable]
public class PatrollingDB
{
    // struttura patrolling
    public Transform target;
    [Tooltip("Il ritardo in cui l'agente si sposta da una transform all'altra")]
    public float delay;
}

public class Patrolling : MonoBehaviour
{
    /// <summary>
    /// 0 = ERROR | 1 = Spostamento sulla transform desiderata e stop | 2+ = Patrolling
    /// </summary>

    [Tooltip("Array di transform su cui l'agente si sposta               (0 = ERROR | 1 = Spostamento sulla transform desiderata e stop | 2+ = Patrolling")]
    public PatrollingDB[] patrollingTransform;

    //public Transform[] patrollingTrans;


    public float delay;
    private int currentPoint = 0;

    // Fuori dalla struttura
    public float delayStartPatrol = 0;
    [HideInInspector]
    public bool isPatrolling = false;
    public bool startOnPlay = false;

    [HideInInspector]
    public GameObject obj;


    NavMeshAgent refNav;
    [Tooltip("Non toccare se non vuoi la console tempestata!!!")]
    public bool debug = false;

    // La coroutine non partirà se inExec sarà true
    private bool inExec = false;

  

    void Awake()
    {
        refNav = GetComponent<NavMeshAgent>();
        obj = this.gameObject;

    }


    void Start()
    {
        if (patrollingTransform.Length == 0)
        {
            Debug.LogError("Inserire almeno una transform nell'array altrimenti non funziona (Patrolling)");
        }
        else
        {
            StartNStopPatrolling(startOnPlay);
        }

        if (debug)
        {
            StartCoroutine(DebuggingMode());
        }
    }

    void GoToNextPoint()
    {
        currentPoint = (currentPoint + 1) % patrollingTransform.Length;
    }

    IEnumerator PatrolingCO()
    {
        if (inExec)
        {
            yield return new WaitForSeconds(delayStartPatrol);
        }
        
        inExec = true;
        
        while (isPatrolling)
        {
            refNav.destination = patrollingTransform[currentPoint].target.position;

            
            yield return null;
            while (refNav.remainingDistance >= refNav.stoppingDistance)
            {
                obj.GetComponent<CharController>().Move(refNav.desiredVelocity, false);
                if (obj.tag == "Player")
                {
                    StopAllCoroutines();
                    refNav.enabled = false;
                }
                //Debug.Log("ciao");
                yield return null;
            }

            obj.GetComponent<Animator>().SetFloat("Forward",0);
            yield return new WaitForSeconds(patrollingTransform[currentPoint].delay);
            GoToNextPoint();
            if (patrollingTransform.Length == 1)
            {
                isPatrolling = false;
            }

        }
        
        inExec = false;
        refNav.enabled = false;
    }


    public void StartNStopPatrolling(bool patrol)
    {
        // controllo tag
        isPatrolling = patrol;
        if (!refNav.enabled)
        {
            refNav.enabled = true;
            obj.GetComponent<CharController>().enabled = true;
        }
        if (isPatrolling)
        {
            StopAllCoroutines();
            currentPoint = 0;
            StartCoroutine(PatrolingCO());
        }

    }

    IEnumerator DebuggingMode()
    {
        while (debug)
        {
            Debug.Log("Current Point in Transform : " + currentPoint + "| Am i patrolling : " + isPatrolling+ "| Remaining distance: " + refNav.remainingDistance);
            yield return null;
        }
    }

}
