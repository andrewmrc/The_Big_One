using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {

    public List<GameObject> players;
    public List<GameObject> avaiableTarget;
    public List<GameObject> items;
    private List<Speaking> spokedList;
    private List<Analizing> analizedList;

    private List<bool> sequency;
    private int progress;

    public int numberOfSequence;

    #region inspector add-on
    public string addNote = "Aggiungi nota";
    [Tooltip("This is THE VALUE!")]
    [ContextMenuItem("reset", "resetTheValue")]    
    public int num;
    private void resetTheValue()
    {
        num = 0;
    }
    #endregion


    //having messo solo condizione ipotetica in cui dobbiamo raccogliere oggetti
    public enum MyEvent { being, having, speaking};
    public MyEvent myEvent;
    public bool inOrder;

    #region UnityFunction
    void Awake()
    {
        spokedList = new List<Speaking>();
        progress = 0;
        
        DeleteNullObjects();
        sequency = new List<bool>();
        for(int i = 0; i < items.Count; i++)
        {
            sequency.Add(false);
        }
    }

    void Update()
    {
        if (num == FindObjectsOfType<GameFlow>().Length - 1)
        {
            switch (myEvent)
            {
                case MyEvent.being:
                    if (inOrder)
                    {
                        CheckBeingOrdered();
                    }
                    else
                    {
                        CheckBeing();
                    }
                    break;
                case MyEvent.speaking:
                    if (inOrder)
                    {
                        CheckSpokedListOrdered();
                    }
                    else
                    {
                        CheckSpokedList();
                    }
                    break;
            }
        }
    }
    #endregion
    #region controllo del corpo
    private void CheckBeing()
    {
        for(int i = 0; i < items.Count; i++)
        {
			PowerController isBodyControlled = items[i].GetComponent<PowerController>();
            if(isBodyControlled && isBodyControlled.isActiveAndEnabled)
            {
                sequency[i] = true;
            }
        }
        End();
    }

    private void CheckBeingOrdered()
    {
        PowerController isBodyControlled = items[progress].GetComponent<PowerController>();
        if (isBodyControlled != null && isBodyControlled.isActiveAndEnabled)
        {
            if (progress == 0 || MyGlobal.oldBody == items[progress - 1])
            {
                sequency[progress++] = true;
            }
        }
        End();
    }
    #endregion
    #region dialoghi
    private void CheckSpokedList()
    {   //in disordine e senza controllo di chi è che parla
        for (int i = 0; i < items.Count; i++)
        {
            Speaking s = items[i].GetComponent<Speaking>();
            if (s)
            {
                foreach (Speaking q in spokedList)
                {
                    if (s == q) //PERICOLO!!!
                    {
                        sequency[i] = true;
                    }
                }
            }
        }
        End();
    }
    private void CheckSpokedListOrdered()
    {   //in ordine con controllo degli interlocutori
        Speaking s = avaiableTarget[progress].GetComponent<Speaking>();
        
        foreach (Speaking q in spokedList)
        {
            if (s == q) //PERICOLO!!!
            {
                if (MyGlobal.myBody == players[progress])
                {
                    progress++;
                    break;
                }
            }
        }
        if(progress == avaiableTarget.Count)
        {
            Destroy(this);
        }
    }
    public void AddSpokenObject(Speaking s)
    {
        spokedList.Add(s);
    }
    #endregion
    #region interazione mappa
    #endregion
    private void End()
    {
        int i = 0;
        while (i < sequency.Count && sequency[i]) i++;
        if (i == sequency.Count)
        {
            Destroy(this);
        }
    }

    private void DeleteNullObjects()
    {
        int i = 0;
        while (i < items.Count)
        {
            if (items[i])
            {
                i++;
            }
            else
            {
                items.RemoveAt(i);
            }
        }
    }
    
}
