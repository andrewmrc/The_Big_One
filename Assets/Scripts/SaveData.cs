using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


public class SaveData : MonoBehaviour
{

    [Serializable]
    class PlayerData
    {
        public Dictionary<string, Dictionary<string, float>> npcInfo;
        public string playername;

        public PlayerData()
        {
            npcInfo = new Dictionary<string, Dictionary<string, float>>();
        }

        public void addNpcInfo(string name, Dictionary<string, float> inf)
        {
            npcInfo.Add(name, inf);
        }

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
                    GameObject.Find(playername).GetComponent<FSMLogic>().enabled = true;
                    GameObject.FindObjectOfType<FreeLookCam>().m_Target = GameObject.Find(playername).transform;
                }

                else
                {
                    GameObject.Find(playername).tag = "ControllableNPC";
                    GameObject.Find(playername).GetComponent<FSMLogic>().enabled = false;
                    GameObject.Find(playername).GetComponent<CharController>().enabled = false;
                }
            }
        }
    }
}