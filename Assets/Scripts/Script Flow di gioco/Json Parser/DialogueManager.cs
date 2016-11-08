using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ConversationHandler;
using System;

public class DialogueManager : MonoBehaviour
{

    //debug var
    public string actualCharacter;
    public int characterID;
    public string actualTarget;
    public int targetID;
    public int moveThroughNodes;
    public Dialogue actualNode;
    public string actualSeq;
    public string actualOut;

    //system var
    public Dialogue actualDialogue;
    public Text dialogue;
    public Text DisplayedName;
    public float letterPause = 0.2f;
    private bool isDiagRunning;
    string message;
    Conversation cv = new Conversation("Inizio", "dialoghi iniziali di prova");



    // Use this for initialization
    void Start()
    {

        cv.parseJSON("test.json");
        actualNode = cv.getDialogue(0);
        //cv.debugDialogues();

    }

    // Update is called once per frame
    void Update()
    {
        actualCharacter = GameObject.FindGameObjectWithTag("Player").name;
        characterID = CharacterToID(actualCharacter);
        RaycastTarget();
        if (Input.GetKeyDown(KeyCode.S))
        {
            actualDialogue = getNextDialogue();

            Debug.Log("charID, actorID; aimID, conversantID:" +
                characterID.ToString() + ", " +
                actualDialogue.actor + "; " +
                targetID.ToString() + ", " +
                actualDialogue.conversant);

            if (characterID.ToString().Equals(actualDialogue.actor) &&
                targetID.ToString().Equals(actualDialogue.conversant))
            {
                printDialogue(actualDialogue);
            }
        }

    }


    public Dialogue getNextDialogue()
    {

        List<Dialogue> actualNode_children = cv.getChildrenWithSequence(actualNode);
        actualNode = actualNode_children[0];
        return actualNode;
    }

    public void printDialogue(Dialogue dialog_to_print)
    {

        Debug.Log("Sequenza, Output, Tag, Testo: " +
                   dialog_to_print.getSequence() + ", " +
                   dialog_to_print.getOutput() + ", " +
                   dialog_to_print.menu_text + ", " +
                   dialog_to_print.text);
        actualSeq = dialog_to_print.getSequence();
        actualOut = dialog_to_print.getOutput();
        DisplayedName.text = dialog_to_print.menu_text;
        dialogue.text = dialog_to_print.text;
        //starta coroutine che stampa dialogo a schermo
        //correggere il fatto che scarta il dialogo precedente anche se parli con il nodo sbagliato incrementa il dialogo
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
    public int CharacterToID(string _actualCharacter)
    {
        int charID = 0;
        switch (_actualCharacter)
        {
            case "Olivia_Player":
                charID = 1;
                break;
            case "NPC_Receptionist":
                charID = 2;
                break;
            case "NPC_Casual":
                charID = 3;
                break;
        }
        return charID;
    }
    public void RaycastTarget()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 20))
        {
            if (hit.collider.tag == "ControllableNPC")
            {
                Debug.DrawLine(ray.origin, hit.point, Color.black);
                Debug.Log(hit.collider.name + ", " + hit.collider.tag);
                actualTarget = hit.collider.name;
                targetID = CharacterToID(actualTarget);
            }

        }

    }
}