using UnityEngine;
using System.Collections;

public class BooleanInventory : MonoBehaviour
{
    Inventory refInventory;

    bool[] objectBoolArray;

	void Start ()
    {
        refInventory = FindObjectOfType<Inventory>();

        for (int i = 0; i < refInventory.designInventory.Length; i++)
        {
            objectBoolArray[i] = refInventory.designInventory[i].isFound;
        }
    }
	
}
