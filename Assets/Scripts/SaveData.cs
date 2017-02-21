using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SaveData : MonoBehaviour
{
    //private FlowManager flow;
    private DoorHandler[] doorsToSave;
    public List<GameObject> ActiveItemList;
    public int idSlot = 0;

    [Serializable]
    class DoorData
    {
        public bool playerCanEnter;
        public bool isFreeForNpc;
        public List<string> listOfGo;


        public DoorData()
        {
            playerCanEnter = false;
            isFreeForNpc = false;
            listOfGo = new List<string>();
        }
    }


    //[Serializable]
    //class FlowBoolSave
    //{
    //    public FlowBoolSave(bool[] _sequence, bool _executed)
    //    {
    //        sequence = _sequence;
    //        executed = _executed;

    //    }
    //    public bool[] sequence;
    //    public bool executed;
    //}

    [Serializable]
    class PlayerData
    {
        public Dictionary<string, Dictionary<string, float>> npcInfo;
        public string playername;
        //public List<FlowBoolSave> sequenceToSave;
        public Dictionary<string, Dictionary<string, bool>> components;
        public Dictionary<string, DoorData> doors;
        public Dictionary<string, Dictionary<string, bool>> executed_bools;
        public string sceneName;

        public Dictionary<string, bool> superDict;


        public PlayerData()
        {
            npcInfo = new Dictionary<string, Dictionary<string, float>>();
            components = new Dictionary<string, Dictionary<string, bool>>();
            doors = new Dictionary<string, DoorData>();
            superDict = new Dictionary<string, bool>();
            executed_bools = new Dictionary<string, Dictionary<string, bool>>();
        }

        public void addNpcInfo(string name, Dictionary<string, float> inf)
        {
            npcInfo.Add(name, inf);
        }

        public void addComponent(string npc, string name, bool enabled)
        {
            if (!components.ContainsKey(npc))
            {
                components[npc] = new Dictionary<string, bool>();
            }

            if (!components[npc].ContainsKey(name))
                components[npc].Add(name, enabled);
        }

        public void addDoor(string uniqueId, DoorData door)
        {
            if (!doors.ContainsKey(uniqueId))
                doors.Add(uniqueId, door);
        }
        public void addActiveItems(string name, bool enableStatus)
        {
            if (!superDict.ContainsKey(name))
                superDict.Add(name, enableStatus);
        }

        public void addExecuted(string obj, string className, bool exec)
        {
            if (!executed_bools.ContainsKey(obj))
            {
                Dictionary<string, bool> dictTmp = new Dictionary<string, bool>();
                dictTmp.Add(className, exec);
                executed_bools.Add(obj, dictTmp);
            }
            else
            {
                Dictionary<string, bool> dictTmp = executed_bools[obj];
                if (!dictTmp.ContainsKey(className))
                {
                    executed_bools[obj].Add(className, exec);
                }
            }
        }
    }

    void Awake()
    {
        doorsToSave = FindObjectsOfType<DoorHandler>();
        //flow = FindObjectOfType<FlowManager>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Save(idSlot);

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Load(idSlot);
        }

    }

    public void Save(int idSave)
    {
        string saveName = "/" + Convert.ToString(idSave) + ".txt";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + saveName);

        PlayerData data = new PlayerData();

        //salvo il nome della scena
        data.sceneName = SceneManager.GetActiveScene().name;
        idSlot = idSave;
        //salvo il flowmanager
        //if (flow)
        //{
        //    data.sequenceToSave = new List<FlowBoolSave>();
        //    foreach (var array in flow.flowRandomGameArray)
        //    {
        //        data.sequenceToSave.Add(new FlowBoolSave(array.sequence, array.executed));
        //    }
        //    foreach (var array in flow.flowGameArray)
        //    {
        //        data.sequenceToSave.Add(new FlowBoolSave(array.sequence, array.executed));
        //    }
        //}


        var enemyPaths = GameObject.FindObjectsOfType<FSM_EnemyPath>();
        foreach (var entmp in enemyPaths)
        {
            if (entmp != null)
            {
                data.addExecuted("FSM_EnemyPath", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }

        var examinables = GameObject.FindObjectsOfType<Examinable>();
        foreach (var entmp in examinables)
        {
            if (entmp != null)
            {
                data.addExecuted("Examinable", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }

        var dialogueshandlers = GameObject.FindObjectsOfType<DialogueHandler>();
        foreach (var entmp in dialogueshandlers)
        {
            if (entmp != null)
            {
                data.addExecuted("DialogueHandler", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }

        var statesmemories = GameObject.FindObjectsOfType<State_ShowMemory>();
        foreach (var entmp in statesmemories)
        {
            if (entmp != null)
            {
                data.addExecuted("State_ShowMemory", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }

        var triggeractions = GameObject.FindObjectsOfType<TriggerAction>();
        foreach (var entmp in triggeractions)
        {
            if (entmp != null)
            {
                data.addExecuted("TriggerAction", entmp.name, entmp.executed);
				//Debug.Log("AGGIUNTO: " + entmp.name + entmp.executed);
            }

        }

        var doorhandlersex = GameObject.FindObjectsOfType<DoorHandler>();
        foreach (var entmp in doorhandlersex)
        {
            if (entmp != null)
            {
                data.addExecuted("DoorHandler", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }

        var railcameras = GameObject.FindObjectsOfType<RailCamera>();
        foreach (var entmp in railcameras)
        {
            if (entmp != null)
            {
                data.addExecuted("RailCamera", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }


        var gameevents = GameObject.FindObjectsOfType<GameEvents>();
        foreach (var entmp in gameevents)
        {
            if (entmp != null)
            {
                data.addExecuted("GameEvents", entmp.name, entmp.executed);
                //Debug.Log("AGGIUNTO: " + entmp.name);
            }

        }


        var npcList = GameObject.FindGameObjectsWithTag("ControllableNPC");
        foreach (var npc in npcList)
        {
            Dictionary<string, float> npcInfo = new Dictionary<string, float>();
            var npcpos = npc.transform.position;
            npcInfo.Add("posx", npcpos.x);
            npcInfo.Add("posy", npcpos.y);
            npcInfo.Add("posz", npcpos.z);
            var npcrot = npc.transform.rotation;
            npcInfo.Add("rotx", npcrot.x);
            npcInfo.Add("roty", npcrot.y);
            npcInfo.Add("rotz", npcrot.z);
            var nva = npc.GetComponent<UnityEngine.NavMeshAgent>();
            if (nva != null)
            {
                npcInfo.Add("destx", nva.destination.x);
                npcInfo.Add("desty", nva.destination.y);
                npcInfo.Add("destz", nva.destination.z);
            }
            data.addNpcInfo(npc.name, npcInfo);

            var compsList = npc.GetComponents<MonoBehaviour>();
            foreach (var comp in compsList)
            {
                data.addComponent(npc.name, comp.ToString(), comp.enabled);
            }
        }

        if (ActiveItemList.Count > 0)
        {
            foreach (var item in ActiveItemList)
            {
                data.superDict.Add(item.name, item.activeSelf);
                foreach (Transform child in item.transform)
                {
                    if (!data.superDict.ContainsKey(child.name))
                        data.superDict.Add(child.name, child.gameObject.activeSelf);
                    Debug.Log("CHILD: " + child.name);
                }
                Debug.Log(item.name + item.activeSelf);
            }
        }

        if (doorsToSave != null)
            foreach (DoorHandler doorTmp in doorsToSave)
            {
                DoorData dd = new DoorData();
                //Debug.Log("TROVATA PORTA " + doorTmp.uniqueId + " booleani: " + doorTmp.isFreeForNpc + " , " + doorTmp.playerCanEnter);
                if (doorTmp.uniqueId != null)
                {
                    dd.isFreeForNpc = doorTmp.isFreeForNpc;
                    dd.playerCanEnter = doorTmp.playerCanEnter;
                    dd.listOfGo = new List<string>();
                    foreach (GameObject goTmp in doorTmp.listOfGo)
                    {
                        if (goTmp != null)
                        {
                            //Debug.Log("TROVATO GO " + goTmp);
                            dd.listOfGo.Add(goTmp.name);
                        }
                    }
                    data.addDoor(doorTmp.uniqueId, dd);
                }
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

        var comps = playerNPC.GetComponents<MonoBehaviour>();
        foreach (var comp in comps)
        {
            data.addComponent(playerNPC.name, comp.ToString(), comp.enabled);
        }

        bf.Serialize(fs, data);
        fs.Close();
    }

    private void onSceneLoaded(Scene s, LoadSceneMode e)
    {
        SceneManager.SetActiveScene(s);
        //Debug.Log("SCENA CARICATA: " + s.name);
    }

    public void Load(int idSave)
    {
        // Set timescale to 1 if Load is called by the UI button
        Time.timeScale = 1;
        string saveName = Application.persistentDataPath + "/" + Convert.ToString(idSave) + ".txt";

        if (File.Exists(saveName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(saveName, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(fs);
            fs.Close();
            var actualScene = SceneManager.GetActiveScene().name;

            //Debug.Log("PRIMA DEL CARICAMENTO SCENA "+actualScene);
            if (actualScene != data.sceneName)
            {
                SceneManager.sceneLoaded += onSceneLoaded;
                SceneManager.LoadScene(data.sceneName, LoadSceneMode.Single);
                return;
            }

            //Debug.Log("DOPO IL CARICAMENTO SCENA "+data.sceneName);

            //setto il flow manager
            //if (flow)
            //{
            //    int i;
            //    for (i = 0; i < FlowManager.Self.flowRandomGameArray.Length; i++)
            //    {
            //        flow.flowRandomGameArray[i].sequence = data.sequenceToSave[i].sequence;
            //        flow.flowRandomGameArray[i].executed = data.sequenceToSave[i].executed;
            //    }
            //    int j = 0;
            //    for (i = FlowManager.Self.flowRandomGameArray.Length; j < FlowManager.Self.flowGameArray.Count; i++)
            //    {

            //        flow.flowGameArray[j].sequence = data.sequenceToSave[i].sequence;
            //        flow.flowGameArray[j].executed = data.sequenceToSave[i].executed;
            //        j++;
            //    }
            //}

            var doorMap = data.doors;

            if (doorsToSave != null && doorMap != null)
                foreach (DoorHandler doorTmp in doorsToSave)
                {
                    if (doorTmp != null && doorTmp.uniqueId != null)
                    {
                        if (doorMap.ContainsKey(doorTmp.uniqueId))
                        {
                            DoorData ddTmp = doorMap[doorTmp.uniqueId];
                            if (ddTmp != null)
                            {
                                doorTmp.playerCanEnter = ddTmp.playerCanEnter;
                                doorTmp.isFreeForNpc = ddTmp.isFreeForNpc;
                                if (ddTmp.listOfGo != null)
                                {
                                    doorTmp.listOfGo = new List<GameObject>();
                                    foreach (string goTmp in ddTmp.listOfGo)
                                    {
                                        doorTmp.listOfGo.Add(GameObject.Find(goTmp));
                                    }
                                }
                            }
                        }
                    }
                }

            var itemDict = data.superDict;
            if (itemDict != null)
            {
                foreach (var kvp in itemDict)
                {
                    //Debug.Log("KVP: " + kvp);
                    //bool isActive = itemDict[item];
                    var goTmp = GameObject.Find(kvp.Key);
                    Debug.Log("GAME OBJECT ACTIVE: " + goTmp);
                    if (goTmp != null)
                        goTmp.SetActive(kvp.Value);
                }
            }

            var executedDict = data.executed_bools;

            if (executedDict.ContainsKey("FSM_EnemyPath"))
            {
                var enemyPaths = GameObject.FindObjectsOfType<FSM_EnemyPath>();
                foreach (var entmp in enemyPaths)
                {
                    if (executedDict["FSM_EnemyPath"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["FSM_EnemyPath"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("Examinable"))
            {
                var examinables = GameObject.FindObjectsOfType<Examinable>();
                foreach (var entmp in examinables)
                {
                    if (executedDict["Examinable"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["Examinable"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("DialogueHandler"))
            {
                var dialogues = GameObject.FindObjectsOfType<DialogueHandler>();
                foreach (var entmp in dialogues)
                {
                    if (executedDict["DialogueHandler"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["DialogueHandler"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("State_ShowMemory"))
            {
                var statememories = GameObject.FindObjectsOfType<State_ShowMemory>();
                foreach (var entmp in statememories)
                {
                    if (executedDict["State_ShowMemory"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["State_ShowMemory"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("TriggerAction"))
            {
                var triggers = GameObject.FindObjectsOfType<TriggerAction>();
                foreach (var entmp in triggers)
                {
                    if (executedDict["TriggerAction"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["TriggerAction"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("DoorHandler"))
            {
                var doorshandlerex = GameObject.FindObjectsOfType<DoorHandler>();
                foreach (var entmp in doorshandlerex)
                {
                    if (executedDict["DoorHandler"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["DoorHandler"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("RailCamera"))
            {
                var railcameras = GameObject.FindObjectsOfType<RailCamera>();
                foreach (var entmp in railcameras)
                {
                    if (executedDict["RailCamera"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["RailCamera"][entmp.name];

                }
            }

            if (executedDict.ContainsKey("GameEvents"))
            {
                var gameevents = GameObject.FindObjectsOfType<GameEvents>();
                foreach (var entmp in gameevents)
                {
                    if (executedDict["GameEvents"].ContainsKey(entmp.name))
                        entmp.executed = executedDict["GameEvents"][entmp.name];

                }
            }

            //sposto il player
            var allInfo = data.npcInfo;
            foreach (string playername in allInfo.Keys)
            {
                var playerInfo = allInfo[playername];
                //Debug.Log("PLAYERNAME " + SceneManager.GetActiveScene().name);
                Vector3 newpos = new Vector3(playerInfo["posx"], playerInfo["posy"], playerInfo["posz"]);
                Quaternion newrot = new Quaternion(0, playerInfo["roty"], 0, 1);
                GameObject.Find(playername).transform.position = newpos;
                GameObject.Find(playername).transform.rotation = newrot;

                var comps = GameObject.Find(playername).GetComponents<MonoBehaviour>();
                for (var i = 0; i < comps.Length; i++)
                {
                    var comp = comps[i];
                    var compname = comp.ToString();
                    if (data.components.ContainsKey(playername))
                    {
                        var actcomp = data.components[playername];
                        if (actcomp.ContainsKey(compname))
                        {
                            GameObject.Find(playername).GetComponents<MonoBehaviour>()[i].enabled = actcomp[compname];
                        }
                    }
                }

                if (playername.Equals(data.playername))
                {
                    GameObject.Find(playername).tag = "Player";
                    GameObject.FindObjectOfType<FreeLookCam>().m_Target = GameObject.Find(playername).transform;


                    //DA SISTEMARE CON IL NOME DELL'NPC PLAYER  <-----------------------------------------!!
                    if (playername != "OliviaRig_Stand")
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
                        var nva = GameObject.Find(playername).GetComponent<UnityEngine.NavMeshAgent>();
                        if (nva != null && nva.isActiveAndEnabled && nva.isOnNavMesh)
                        {
                            nva.SetDestination(newdest);
                        }
                    }
                    GameObject.Find(playername).tag = "ControllableNPC";
                }
            }
        }
    }
    public void setIdSlot(int _idSlot)
    {
        idSlot = _idSlot;
    }
}