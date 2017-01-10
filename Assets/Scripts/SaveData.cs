using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class SaveData : MonoBehaviour
{
    public FlowManager flow;

    [Serializable]
    class FlowBoolSave
    {
        public FlowBoolSave(bool[] _sequence,bool _executed)
        {
            sequence = _sequence;
            executed = _executed;
            
        }
        public bool[] sequence;
        public bool executed;
    }

    [Serializable]
    class PlayerData
    {
        public Dictionary<string, Dictionary<string, float>> npcInfo;
        public string playername;
        public List<FlowBoolSave> sequenceToSave; 
        

        public PlayerData()
        {
            npcInfo = new Dictionary<string, Dictionary<string, float>>();
        }

        public void addNpcInfo(string name, Dictionary<string, float> inf)
        {
            npcInfo.Add(name, inf);
        }

        //public void AddFlowManager()
        //{
        //    sequenceToSave = new List<FlowBoolSave>();
        //    foreach (var array in FlowManager.Self.flowRandomGameArray)
        //    {
        //        sequenceToSave.Add(new FlowBoolSave(array.sequence,array.executed));                
        //    }
        //    foreach (var array in FlowManager.Self.flowGameArray)
        //    {                
        //        sequenceToSave.Add(new FlowBoolSave(array.sequence, array.executed));  
        //    }           
            
        //}

    }

    void Awake()
    {
        flow = FindObjectOfType<FlowManager>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Save();
            
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Load();
        }

    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/Playerdata.txt");

        PlayerData data = new PlayerData();

        //data.AddFlowManager();
        //salvo il flowmanager
        if (flow)
        {
            data.sequenceToSave = new List<FlowBoolSave>();
            foreach (var array in flow.flowRandomGameArray)
            {
                data.sequenceToSave.Add(new FlowBoolSave(array.sequence, array.executed));
            }
            foreach (var array in flow.flowGameArray)
            {
                data.sequenceToSave.Add(new FlowBoolSave(array.sequence, array.executed));
            }
        }
        
        var npcList = GameObject.FindGameObjectsWithTag("ControllableNPC");
        foreach (var npc in npcList)
        {
            //Debug.Log ("NOME: "+npc.name+ ", POSIZIONE: "+npc.transform.position);
            Dictionary<string, float> npcInfo = new Dictionary<string, float>();
            var npcpos = npc.transform.position;
            npcInfo.Add("posx", npcpos.x);
            npcInfo.Add("posy", npcpos.y);
            npcInfo.Add("posz", npcpos.z);
            var npcrot = npc.transform.rotation;
            npcInfo.Add("rotx", npcrot.x);
            npcInfo.Add("roty", npcrot.y);
            npcInfo.Add("rotz", npcrot.z);
            var nva = npc.GetComponent<NavMeshAgent>();
            if (nva != null)
            {
                npcInfo.Add("destx", nva.destination.x);
                npcInfo.Add("desty", nva.destination.y);
                npcInfo.Add("destz", nva.destination.z);
            }
            data.addNpcInfo(npc.name, npcInfo);

        }

        var playerNPC = GameObject.FindGameObjectWithTag("Player");
        Dictionary<string, float> playerInfo = new Dictionary<string, float>();
        var playerpos = playerNPC.transform.position;
        playerInfo.Add("posx", playerpos.x);
        playerInfo.Add("posy", playerpos.y);
        playerInfo.Add("posz", playerpos.z);
        var playerrot = playerNPC.transform.rotation;
        playerInfo.Add("roty", playerrot.y);
        data.addNpcInfo(playerNPC.name, playerInfo);
        data.playername = playerNPC.name;
        
        bf.Serialize(fs, data);
        fs.Close();

    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Playerdata.txt"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/Playerdata.txt", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(fs);
            fs.Close();

            //setto il flow manager
            if (flow)
            {
                int i;
                for (i = 0; i < FlowManager.Self.flowRandomGameArray.Length; i++)
                {
                    flow.flowRandomGameArray[i].sequence = data.sequenceToSave[i].sequence;
                    flow.flowRandomGameArray[i].executed = data.sequenceToSave[i].executed;
                }
                int j = 0;
                for (i = FlowManager.Self.flowRandomGameArray.Length; j < FlowManager.Self.flowGameArray.Count; i++)
                {

                    flow.flowGameArray[j].sequence = data.sequenceToSave[i].sequence;
                    flow.flowGameArray[j].executed = data.sequenceToSave[i].executed;
                    j++;
                }
            }            

            //sposto il player
            var allInfo = data.npcInfo;
            foreach (string playername in allInfo.Keys)
            {
                var playerInfo = allInfo[playername];
                Vector3 newpos = new Vector3(playerInfo["posx"], playerInfo["posy"], playerInfo["posz"]);
                Quaternion newrot = new Quaternion(0, playerInfo["roty"], 0, 1);
                GameObject.Find(playername).transform.position = newpos;
                GameObject.Find(playername).transform.rotation = newrot;
                if (playername.Equals(data.playername))
                {
                    GameObject.Find(playername).tag = "Player";                    
                    GameObject.FindObjectOfType<FreeLookCam>().m_Target = GameObject.Find(playername).transform;
                    GameObject.Find(playername).GetComponent<FSMLogic>().enabled = true;

                    //DA SISTEMARE CON IL NOME DELL'NPC PLAYER  <-----------------------------------------!!
                    if (playername != "OliviaRigCorretta")
                    {
                        GameManager.Self.outOfYourBody = true;
                    }
                }

                else
                {
                    if (playerInfo.ContainsKey("destx"))
                    {
                        var destx = playerInfo["destx"];
                        var desty = playerInfo["desty"];
                        var destz = playerInfo["destz"];
                        Vector3 newdest = new Vector3(destx, desty, destz);
                        var nva = GameObject.Find(playername).GetComponent<NavMeshAgent>();
                        if (nva != null && nva.isActiveAndEnabled && nva.isOnNavMesh)
                            nva.SetDestination(newdest);

                    }
                    GameObject.Find(playername).tag = "ControllableNPC";
                    GameObject.Find(playername).GetComponent<FSMLogic>().enabled = false;
                    GameObject.Find(playername).GetComponent<CharController>().enabled = false;
                }
            }
        }
    }
}