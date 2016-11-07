using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    //debug var
    public int moveThroughNodes;
    public int actualNode;
    public string actualSeq;
    public string actualOut;
    
    //system var
    public Text dialogue;
    public Text DisplayedName;
    public float letterPause = 0.2f;
    private bool isDiagRunning;
    string message;
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Dialogue()
    {
        string[] Sentece;
        if (/*controllo su player e su npc && controllo su Sequence &&*/ isDiagRunning == false )
        {
            //prendi le stringhe dal parser - sequnce, output, nome del tag, dialogo
            //metti in Text DisplayedName il valore del tag;
            //starta coroutine che stampa dialogo a schermo
            //setta il nodo figlio come start point
            //setta la nuova sequence
        }
    }

    //IEnumerator TypeText()
    //{
    //    isDiagRunning = true;
    //    Debug.Log(isDiagRunning);
    //    foreach (char letter in message.ToCharArray())
    //    {
    //        dialogo.text += letter;
    //        yield return new WaitForSeconds(letterPause);
    //    }
    //    yield return new WaitForSeconds(1);
    //    dialogo.text = null;
    //    isDiagRunning = false;
    //}

}
