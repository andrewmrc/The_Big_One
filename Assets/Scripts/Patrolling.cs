using UnityEngine;
using System.Collections;

public class Patrolling : MonoBehaviour
{
    /// <summary>
    /// 0 = ERROR | 1 = Spostamento sulla transform desiderata e stop | 2+ = Patrolling
    /// </summary>

    [Tooltip("Array di transform su cui l'agente si sposta               (0 = ERROR | 1 = Spostamento sulla transform desiderata e stop | 2+ = Patrolling")]
    public Transform[] patrollingTrans;
    [Tooltip("Il ritardo in cui l'agente si sposta da una transform all'altra")]
    public float delay;
    private int currentPoint = 0;

    public bool startOnPlay = false;

    [HideInInspector]
    public bool isPatrolling = false;

    NavMeshAgent refNav;
    [Tooltip("Non toccare se non vuoi la console tempestata!!!")]
    public bool debug = false;

    void Awake()
    {
        refNav = GetComponent<NavMeshAgent>();

    }


    void Start()
    {
        if (patrollingTrans.Length == 0)
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
        currentPoint = (currentPoint + 1) % patrollingTrans.Length;
    }

    IEnumerator PatrolingCO()
    {

        while (isPatrolling)
        {
            Debug.Log("Sono dentro il primo while 1");
            refNav.destination = patrollingTrans[currentPoint].position;
            yield return null;
            while (refNav.remainingDistance >= refNav.stoppingDistance)
            {
                Debug.Log("Sono dentro il secondo while 2");
                yield return null;
            }
            GoToNextPoint();
            yield return new WaitForSeconds(delay);
            if (patrollingTrans.Length == 1)
            {
                isPatrolling = false;
            }

        }
    }

    public void StartNStopPatrolling(bool patrol)
    {
        isPatrolling = patrol;
        if (!refNav.enabled)
        {
            refNav.enabled = true;
        }
        if (isPatrolling)
        {
            StartCoroutine(PatrolingCO());
        }

    }

    IEnumerator DebuggingMode()
    {
        while (true)
        {
            Debug.Log("Current Point in Transform : " + currentPoint + "| Am i patrolling : " + isPatrolling+ "| Remaining distance: " + refNav.remainingDistance);
            yield return null;
        }
    }

}
