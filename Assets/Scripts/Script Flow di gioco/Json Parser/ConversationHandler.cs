using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;
using System;

namespace ConversationHandler
{
    //Classe relativa ai dialoghi
    public class Dialogue
    {
        public int ID;
        public string actor;
        public string conversant;
        public string text;
        public string menu_text;
        public HashSet<string> sequence;
        public HashSet<string> output;
        public HashSet<int> outLinks;

        public Dialogue(int id)
        {
            this.ID = id;
            this.actor = "";
            this.conversant = "";
            this.text = "";
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

        public void InsertOutLinks(int link)
        {
            outLinks.Add(link);
        }

        public List<int> getOutLink()
        {
            return outLinks.ToList<int>();
        }

        //ouput e compreso nella sequence e se compreso restituisce il nuovo out
        public HashSet<string> checkSequence(HashSet<string> prev_output)
        {
            if (sequence.IsSupersetOf(prev_output))
                return output;
            else
                return null;
        }

        public string getOutput()
        {
            string result = "";
            foreach (String o in output)
            {
                result += ", " + o;
            }
            return result;
        }

        public string getSequence()
        {
            string result = "";
            foreach (String s in sequence)
            {
                result += ", " + s;
            }
            return result;
        }
    }


    //Classe per la conversazione globale
    public class Conversation
    {
        private Dictionary<int, Dialogue> dialogues;
        private Dictionary<int, string> actor_names;
        public string title;
        public int ID;

        public Conversation(string title, string description)
        {
            this.dialogues = new Dictionary<int, Dialogue>();
            this.actor_names = new Dictionary<int, string>();
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

        public string getActorName(int id)
        {
            return actor_names[id];
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
            ID = Convert.ToInt16(convs["ID"]);

            List<object> convFields = convField["Field"] as List<object>;

            //scorre lista attributi conversation
            foreach (Dictionary<string, object> d in convFields)
            {
                if (d["Title"].Equals("Title"))
                    this.title = (string)d["Value"];
            }

            List<object> dialogentry = dialogentries["DialogEntry"] as List<object>;

            //scorre tutti i dialoghi nel JSON
            foreach (Dictionary<string, object> dialog in dialogentry)
            {
                int dialogID = Convert.ToInt16(dialog["ID"]);
                Dictionary<string, object> dialogFields = dialog["Fields"] as Dictionary<string, object>;
                List<object> dialogFieldList = dialogFields["Field"] as List<object>;

                //Crea un nuovo dialogo da aggiungere al dizionario
                Dialogue actualDialogue = new Dialogue(dialogID);

                //scorre tutti i campi field nel JSON per ogni dialogo
                foreach (Dictionary<string, object> f in dialogFieldList)
                {
                    string actualDialogTitle = (string)f["Title"];
                    string actualDialogValue = (string)f["Value"];
                    if (actualDialogValue != null)
                        switch (actualDialogTitle)
                        {
                            case "Actor":
                                actualDialogue.actor = actualDialogValue;
                                break;
                            case "Conversant":
                                actualDialogue.conversant = actualDialogValue;
                                break;
                            case "Dialogue Text":
                                actualDialogue.text = actualDialogValue;
                                break;
                            case "Menu Text":
                                actualDialogue.menu_text = actualDialogValue;
                                break;
                            case "Sequence":
                                actualDialogue.InsertSequences(actualDialogValue);
                                break;
                            case "Output":
                                actualDialogue.InsertOutputs(actualDialogValue);
                                break;
                        }
                }

                //Controlla gli Outgoing Links (figli)
                Dictionary<string, object> dialogOutLinks = dialog["OutgoingLinks"] as Dictionary<string, object>;
                try
                {
                    if (dialogOutLinks["Link"] is IList)
                    {
                        //Debug.Log("una lista di figli ;)");
                        List<object> outGoingLinks = dialogOutLinks["Link"] as List<object>;
                        foreach (Dictionary<string, object> link in outGoingLinks)
                        {
                            string destinationDialogID = (string)link["DestinationDialogID"];
                            actualDialogue.InsertOutLinks(Convert.ToInt16(destinationDialogID));
                        }
                    }
                    else
                    {
                        //Debug.Log("Non è una lista, ha solo un figlio :D");
                        Dictionary<string, object> outGoingLink = dialogOutLinks["Link"] as Dictionary<string, object>;
                        string destinationDialogID = (string)outGoingLink["DestinationDialogID"];
                        actualDialogue.InsertOutLinks(Convert.ToInt16(destinationDialogID));
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Lista non trovata, non ci sono figli purtroppo! :(");
                }
                Dictionary<string, object> actors = assets["Actors"] as Dictionary<string, object>;
                List<object> actor = actors["Actor"] as List<object>;

                foreach (Dictionary<string, object> actual_actor in actor)
                {
                    int actual_actor_id = Convert.ToInt16((string)actual_actor["ID"]);
                    if (!actor_names.ContainsKey(actual_actor_id))
                    {
                        string actual_actor_name = "";
                        Dictionary<string, object> actual_actor_fields = actual_actor["Fields"] as Dictionary<string, object>;
                        List<object> actual_actor_field = actual_actor_fields["Field"] as List<object>;
                        foreach (Dictionary<string, object> aaf in actual_actor_field)
                        {
                            if (((string)aaf["Title"]).Equals("Name"))
                            {
                                actual_actor_name = (string)aaf["Value"];
                                break;
                            }
                        }
                        actor_names.Add(actual_actor_id, actual_actor_name);
                    }

                }
                
                this.insertDialogue(actualDialogue);
            }
        }

        public List<Dialogue> getChildren(Dialogue parentD)
        {
            List<Dialogue> children_list = new List<Dialogue>();
            List<int> children = parentD.getOutLink();
            foreach (int figlio in children)
            {
                children_list.Add(this.getDialogue(figlio));
            }
            return children_list;
        }

        public List<Dialogue> getChildrenWithSequence(Dialogue parentD)
        {
            //da fare il controllo sulle foglie
            List<Dialogue> children_list = new List<Dialogue>();
            List<int> children = parentD.getOutLink();
            foreach (int child in children)
            {
                Dialogue child_dialogue = this.getDialogue(child);
                if (child_dialogue.sequence.Contains("&") || child_dialogue.sequence.Count == 0 || child_dialogue.checkSequence(parentD.output) != null)
                    children_list.Add(child_dialogue);
            }
            return children_list;
        }

        public void debugDialogues()
        {
            foreach (Dialogue d in this.dialogues.Values)
            {
                Debug.Log("ID:" + d.ID.ToString());
                Debug.Log("Actor:" + d.actor);
                Debug.Log("Conversant:" + d.conversant);
                Debug.Log("Text:" + d.text);
            }
        }
    }
}