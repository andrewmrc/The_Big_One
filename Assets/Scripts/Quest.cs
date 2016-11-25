using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


[System.Serializable]
public struct QuestContainer
{
    public string titleTexter;
    public bool isComplete;
}

public class Quest : MonoBehaviour
{
    public QuestContainer[] designQuest;
    bool isInMenu;
    public GameObject panelQuest, titleQuest;

	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.J) && !isInMenu)
        {
            // Controlla la lista di Quest e assegna il titolo della prima non completata all'Obiettivo attuale
            for (int i = 0; i < designQuest.Length; i++)
            {
                if (!designQuest[i].isComplete)
                {
                    titleQuest.GetComponent<Text>().text = designQuest[i].titleTexter;
                    break;
                }
            }

            isInMenu = true;
            panelQuest.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.J) && isInMenu)
        {
            isInMenu = false;
            panelQuest.SetActive(false);
        }
	}

    public void SetCompleteQuest(bool completed)
    {
        for (int i = 0; i < designQuest.Length; i++)
        {
            if (!designQuest[i].isComplete)
            {
                designQuest[i].isComplete = completed;
                break;
            }
        }
    }
}
