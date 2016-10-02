using UnityEngine;

public class Speaking : MonoBehaviour {
    
    private GameFlow refGameFlow;

    void Start()
    {
        if (!refGameFlow)
            refGameFlow = GameObject.Find("GameManager").GetComponent<GameFlow>();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            refGameFlow.AddSpokenObject(this);
            ///qui andrebbe fatta la funzione del dialogo fuori dall'update

        }
    }
}
