using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {

    public List<GameObject> items;

    private List<Speaking> spokedList;
    private List<Analizing> analizedList;

    private List<bool> sequency;
    private int progress;
    public int num; //non neccessario

    //having messo solo condizione ipotetica in cui dobbiamo raccogliere oggetti
    public enum MyEvent { being, having, speaking};
    public MyEvent myEvent;
    public bool inOrder;

    #region UnityFunction
    void Awake()
    {
        spokedList = new List<Speaking>();
        progress = 0;
        
        DeleteNullOgbjects();
        sequency = new List<bool>(); for (int i = 0; i < items.Count; i++)
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
            BodyControlPower isBodyControlled = items[i].GetComponent<BodyControlPower>();
            if(isBodyControlled && isBodyControlled.isActiveAndEnabled)
            {
                sequency[i] = true;
            }
        }
        End();
    }

    private void CheckBeingOrdered()
    {
        if(progress < items.Count)
        {
            BodyControlPower isBodyControlled = items[progress].GetComponent<BodyControlPower>();
            if (isBodyControlled != null && isBodyControlled.isActiveAndEnabled)
            {
                sequency[progress++] = true;
            }
        }
        //print("ultimo controllato:" + (progress - 1));
        if (progress == sequency.Count)
        {
            //print("azione eseguita " + num);
            this.enabled = false;
        }
        End();
    }
    #endregion
    #region dialoghi
    private void CheckSpokedList()
    {
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
    {
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

    private void DeleteNullOgbjects()
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
