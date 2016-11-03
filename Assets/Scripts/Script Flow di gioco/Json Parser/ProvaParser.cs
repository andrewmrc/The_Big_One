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
            cv.parseJSON("prova.json");
            Dialogue d1 = new Dialogue(1, "NPC1", "NPC2");
            Dialogue d2 = new Dialogue(2, "NPC1", "porta");
            Dialogue d3 = new Dialogue(3, "NPC1", "tempio");

            actual_out.Add("&");

            d1.InsertSequences("&");
            d1.InsertOutputs("chiave");

            d2.InsertSequences("chiave");
            d2.InsertOutputs("gemma2,gemma3,gemma1");

            d2.InsertSequences("gemma1,gemma2,gemma3");
            d2.InsertOutputs("ok");

            actual_out = d1.checkSequence(actual_out);






            //HashSet<string> h = new HashSet<string> ();
        }
    }
}
