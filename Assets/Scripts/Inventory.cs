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
    private bool isInInventory;
    public GameObject panelInventory;

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
        }

        else if (Input.GetKeyDown(KeyCode.I) && isInInventory)
        {
            Time.timeScale = 1;
            isInInventory = false;
            panelInventory.SetActive(false);
        }
    }
}