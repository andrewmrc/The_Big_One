using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using ConversationHandler;

public class ProvaParser : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HashSet<string> actual_out = new HashSet<string>();

            Conversation cv = new Conversation("Conv", "Prima");
            cv.parseJSON("test.json");
            /*Dialogue d1 = new Dialogue (1,"NPC1", "NPC2");
			Dialogue d2 = new Dialogue (2, "NPC1", "porta");
			Dialogue d3 = new Dialogue (3, "NPC1", "tempio");

			actual_out.Add("&"); 

			d1.InsertSequences ("&");
			d1.InsertOutputs ("chiave");

			d2.InsertSequences ("chiave");
			d2.InsertOutputs ("gemma2,gemma3,gemma1");

			d2.InsertSequences ("gemma1,gemma2,gemma3");
			d2.InsertOutputs ("ok");

			actual_out = d1.checkSequence (actual_out);

*/
            //Prendo la root
            Dialogue root = cv.getDialogue(0);
            List<Dialogue> figli_root = cv.getChildrenWithSequence(root);
            foreach (Dialogue figlio in figli_root)
            {
                List<Dialogue> nipoti_controllati = cv.getChildrenWithSequence(figlio);
                string messaggi = "";
                foreach (Dialogue nipote in nipoti_controllati)
                {
                    messaggi += "\n " + nipote.text;
                }
                Debug.Log("Un figlio di root ha ID " + figlio.ID.ToString() + ", si chiama: " + figlio.actor + " con dialogo: \"" + figlio.text + "\" e ha " + nipoti_controllati.Count + " figli giusti con dialoghi: " + messaggi);


            }

            //cv.debugDialogues ();



            //HashSet<string> h = new HashSet<string> ();
        }
    }
}
