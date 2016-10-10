using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speaking : MonoBehaviour {

    public int numberOfSentence;
    private GameFlow refGameFlow;
    public Text dialogo;
    public void Speak(GameFlow refGameFlow)
    {
        refGameFlow.AddSpokenObject(this);
        //CsvFileReader
        //print("parli con: " + gameObject.name);
        string[] sentences = CsvFileReader.ReadSpecificRow(MyGlobal.pathDialogue, 0, gameObject.name);
        if (sentences != null)
        {
            string dialogue = sentences[refGameFlow.numberOfSequence].Split(MyGlobal.separator)[numberOfSentence++];
            //print(dialogue);
            dialogo.text = gameObject.name + ": " + dialogue;
        }
    }

}
