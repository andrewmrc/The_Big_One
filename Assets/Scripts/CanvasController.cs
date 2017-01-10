using UnityEngine;

public class CanvasController : MonoBehaviour
{
    MenuController refMC;
    Inventory refInventory;
    Quest refQuest;

	void Start ()
    {
        refInventory = FindObjectOfType<Inventory>();
        refMC = FindObjectOfType<MenuController>();
        refQuest = FindObjectOfType<Quest>();
	}

    public void InventoryHandler(bool On)
    {
        refMC.enabled = On;
        refQuest.enabled = On;
        Debug.Log("InventoryEnable");
    }

    public void QuestHandler(bool On)
    {
        refMC.enabled = On;
        refInventory.enabled = On;
        Debug.Log("QuestEnable");
    }

    public void ExitHandler(bool On)
    {
        refInventory.enabled = On;
        refQuest.enabled = On;
        Debug.Log("ExitEnable");
    }
}
