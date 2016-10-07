using UnityEngine;
public class MyGlobal
{
    public static GameObject myBody;
    public static GameObject oldBody;

    public static void ChangeBody(GameObject go)
    {
        oldBody = myBody;
        myBody = go;
    }
}