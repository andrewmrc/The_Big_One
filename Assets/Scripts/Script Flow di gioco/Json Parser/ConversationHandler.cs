using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System;

namespace ConversationHandler
{
    //Classe relativa ai dialoghi
    public class Dialogue
    {
        public int ID;
        public string player;
        public string conversant;
        public HashSet<string> sequence;
        public HashSet<string> output;
        public HashSet<int> outLinks;

        public Dialogue(int id, string player, string conversant)
        {
            this.ID = id;
            this.player = player;
            this.conversant = conversant;
            this.sequence = new HashSet<string>();
            this.output = new HashSet<string>();
            this.outLinks = new HashSet<int>();
        }

        public void InsertSequences(string s)
        {
            string[] seq_to_add = s.Split(',');
            sequence.UnionWith(seq_to_add);
        }

        public void InsertOutputs(string o)
        {
            string[] out_to_add = o.Split(',');
            output.UnionWith(out_to_add);
        }

        public void InsertOutLinks(IEnumerable<int> links)
        {
            outLinks.UnionWith(links);
        }
        //ouput e compreso nella sequence e se compreso restituisce il nuovo out
        public HashSet<string> checkSequence(HashSet<string> prev_output)
        {
            if (sequence.IsSupersetOf(prev_output))
                return output;
            else
                return null;
        }
    }


    //Classe per la conversazione globale
    public class Conversation
    {

        private Dictionary<int, Dialogue> dialogues;
        public string title;
        public int ID;

        public Conversation(string title, string description)
        {
            this.dialogues = new Dictionary<int, Dialogue>();
        }

        private object ReadFromFile(string filename)
        {
            string jsonString = File.ReadAllText(Application.dataPath + "/Resources/Dialogues/" + filename);
            return DiagJson.Deserialize(jsonString);
        }

        public void insertDialogue(Dialogue d)
        {
            dialogues.Add(d.ID, d);
        }

        public Dialogue getDialogue(int id)
        {
            return dialogues[id];
        }
        //livelli di annidamento successivo
        //[] list object
        public void parseJSON(string filename)
        {
            Dictionary<string, object> dict = ReadFromFile(filename) as Dictionary<string, object>;
            Dictionary<string, object> assets = dict["Assets"] as Dictionary<string, object>;
            Dictionary<string, object> conv = assets["Conversations"] as Dictionary<string, object>;
            Dictionary<string, object> convs = conv["Conversation"] as Dictionary<string, object>;
            Dictionary<string, object> convField = convs["Fields"] as Dictionary<string, object>;
            Dictionary<string, object> dialogentries = convs["DialogEntries"] as Dictionary<string, object>;
            //Dictionary<string, object> fields = dialogentries["Fields"] as Dictionary<string, object>;
            ID = Convert.ToInt16(convs["ID"]);

            List<object> convFields = convField["Field"] as List<object>;

            //scorre lista attributi conversation
            foreach (Dictionary<string, object> d in convFields)
            {
                if (d["Title"].Equals("Title"))
                    this.title = (string)d["Value"];
            }

            List<object> dialogentry = dialogentries["DialogEntry"] as List<object>;
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
            //scopre tutti i dialoghi field dei dialoghi
            foreach (Dictionary<string, object> dialog in dialogentry)
            {
                string dialogID = (string)dialog["ID"];
                //Debug.Log(dialog["Value"]);
                lst.Add(dialog["Fields"] as Dictionary<string, object>);
            }

            //List<object> field = fields["Field"] as List<object>;
            //foreach (Dictionary<string, object> q in field)
            //{
            //    Debug.Log(q);
            //}

            for (int i = 0; i < lst.Count; i++)
            {
                List<object> speranza = lst[i]["Field"] as List<object>;
                Debug.Log("inizio campo");
                foreach (Dictionary<string, object> q in speranza)
                {
                    Debug.Log(q["Value"]);
                }
            }
        }

    }
}