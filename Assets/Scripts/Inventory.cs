using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public struct ItemContainer
{
    public Button poster;
    public bool isFound;
}

public class Inventory : MonoBehaviour
{
    public ItemContainer[] designInventory;
    public bool isInInventory;
    public GameObject panelInventory;
    public CanvasController refCanvasController;

    private void Start()
    {
        refCanvasController = FindObjectOfType<CanvasController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isInInventory)
        {
            for (int i = 0; i < designInventory.Length; i++)
            {
                if (designInventory[i].isFound)
                    designInventory[i].poster.gameObject.SetActive(true);

                else
                    designInventory[i].poster.gameObject.SetActive(false);
            }

            Time.timeScale = 0;
            isInInventory = true;
            panelInventory.SetActive(true);
            refCanvasController.InventoryHandler(false);
        }

        else if (Input.GetKeyDown(KeyCode.I) && isInInventory)
        {
            Time.timeScale = 1;
            isInInventory = false;
            panelInventory.SetActive(false);
            refCanvasController.InventoryHandler(true);
        }
    }
}