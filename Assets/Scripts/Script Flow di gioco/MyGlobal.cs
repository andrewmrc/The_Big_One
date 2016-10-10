using UnityEngine;
public class MyGlobal
{
    public static string pathDialogue = Application.dataPath + "/Resources/Dialogues/Dialogues.csv";
    public static char separator = ';';
    public static GameObject myBody;
    public static GameObject oldBody;
    public static GameObject rayCastHit;

    public static void ChangeBody(GameObject go)
    {
        oldBody = myBody;
        myBody = go;
    }
}