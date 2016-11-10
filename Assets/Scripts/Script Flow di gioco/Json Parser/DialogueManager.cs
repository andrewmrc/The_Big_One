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
    public int moveThroughNodes = 0;
    public Dialogue actualNode;
    public string actualSeq;
    public string actualOut;

    //system var
    public Dialogue actualDialogue;
    public Text dialogue;
    public Text DisplayedName;
    public float letterPause = 0.2f;
    public bool isDiagRunning;
    string message;
    Conversation cv = new Conversation("Inizio", "dialoghi iniziali di prova");
    

    void Start()
    {

        cv.parseJSON("ProvaAngelo.json");
        actualNode = cv.getDialogue(moveThroughNodes);
        //cv.debugDialogues();
        //setting
        //actualNode.output = new HashSet<string>();
        //actualNode.InsertOutputs("Ciao");
    }


    void Update()
    {
        actualCharacter = GameObject.FindGameObjectWithTag("Player").name;
        characterID = CharacterToID(actualCharacter);
        RaycastTarget();
        if (Input.GetKeyDown(KeyCode.E) && isDiagRunning == false)
        {
            bool hasSpoken = false;
            List<Dialogue> actual_children = getNextDialogue();
            bool isMultipleChoice = (actual_children.Count > 0 && actual_children[0].sequence.Count == 0 &&
                 characterID.ToString().Equals(actual_children[0].actor) &&
                 targetID.ToString().Equals(actual_children[0].conversant));

            //Debug per prendere il nome dei figli
            foreach (Dialogue child in actual_children)
            {
                int child_id = Convert.ToInt16(child.actor);
                string actor_name = cv.getActorName(child_id);
                Debug.Log("Nome attore: " + actor_name);
            }

            /*Debug.Log("charID, actorID; aimID, conversantID:" + 
                characterID.ToString() + ", " + 
                actualDialogue.actor + "; " + 
                targetID.ToString() + ", " + 
                actualDialogue.conversant);
                */

            if (isMultipleChoice)
            {
                //TODO: Gestisci scelta multipla tra tutti gli actual_children
            }
            else
            {
                //Stampa un solo dialogo se è tra le persone giuste
                foreach (Dialogue child in actual_children)
                {
                    if (characterID.ToString().Equals(child.actor) &&
                       targetID.ToString().Equals(child.conversant))
                    {
                        actualNode = child;
                        printDialogue(actualNode);
                        hasSpoken = true;
                        break;
                    }
                }
            }

            //Ristampa il vecchio dialogo se nessuno ha parlato e se è tra le persone giuste
            if (!hasSpoken && characterID.ToString().Equals(actualNode.actor) &&
                targetID.ToString().Equals(actualNode.conversant))
            {
                printDialogue(actualNode);
            }
        }
    }

    public List<Dialogue> getNextDialogue()
    {

        List<Dialogue> actualNode_children = cv.getChildrenWithSequence(actualNode);
        //actualNode = actualNode_children[0];
        return actualNode_children;
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
        message = dialog_to_print.text;
        StartCoroutine(TypeText());
        //correggere il fatto che scarta il dialogo precedente anche se parli con il nodo sbagliato incrementa il dialogo
    }

    IEnumerator TypeText()
    {
        isDiagRunning = true;
        foreach (char letter in message.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
        yield return new WaitForSeconds(1);
        dialogue.text = null;
        DisplayedName.text = null;
        isDiagRunning = false;

    }

    //da togliere dato che viene gestito tutto dalla nuova funzione
    public int CharacterToID(string _actualCharacter)
    {
        int charID = 0;
        switch (_actualCharacter)
        {
            case "Olivia_V3":
                charID = 1;
                break;
            case "NPC4":
                charID = 2;
                break;
            case "Character_Eyes":
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
                //Debug.Log(hit.collider.name + ", " + hit.collider.tag);
                actualTarget = hit.collider.name;
                targetID = CharacterToID(actualTarget);
            }
        }
    }
}