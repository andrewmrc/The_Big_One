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
    }

    public void QuestHandler(bool On)
    {
        refMC.enabled = On;
        refInventory.enabled = On;
    }

    public void ExitHandler(bool On)
    {
        refInventory.enabled = On;
        refQuest.enabled = On;
    }
}
