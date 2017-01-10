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
    public bool isInQuest;
    public GameObject panelQuest, titleQuest;
    public CanvasController refCanvasController;

    private void Start()
    {
        refCanvasController = FindObjectOfType<CanvasController>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isInQuest)
        {
            for (int i = 0; i < designQuest.Length; i++)
            {
                if (!designQuest[i].isComplete)
                {
                    titleQuest.GetComponent<Text>().text = designQuest[i].titleTexter;
                    break;
                }
            }

            Time.timeScale = 0;
            isInQuest = true;
            panelQuest.SetActive(true);
            refCanvasController.QuestHandler(false);
        }

        else if (Input.GetKeyDown(KeyCode.J) && isInQuest)
        {
            Time.timeScale = 1;
            isInQuest = false;
            panelQuest.SetActive(false);
            refCanvasController.QuestHandler(true);
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