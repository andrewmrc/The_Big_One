using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
/*
 * in csv:
 * colonna 0 = tizio A, 1 = tizio B
 * pos pari parla A->B, dispari B->A (name)
 * usare '&' per più frasi dallo stesso "name"
 * */
public class Speaking : MonoBehaviour
{
    const int zero = 6;
    private int pos;
    private Dictionary<string, int> dictPos = new Dictionary<string, int>();
    private string activeBody;
    public string[] sentences;
    //prima frase da dire; ultima frase da dire; frase cappio attivata da final; fase serve per distinguere i dialoghi A == A
    public enum DialogIndex
    {
        fase = 2, initialSentence, finalSantence, repeatingNodeStart
    };
    public int fase;
    //dialogo = zona dove sarà visualizzato; name = nome di chi sta parlando
    public Text dialogo, tizio_che_parla;

    void Awake()
    {
        activeBody = null;
    }

    public void Speak()
    {
        if (activeBody != GameObject.FindGameObjectWithTag("Player").name)
        {
            activeBody = GameObject.FindGameObjectWithTag("Player").name;
            
        }
        string row = CsvFileReader.ReadDialogue(MyGlobal.pathDialogue, activeBody, gameObject.name, fase.ToString());
        if (row != null)
        {
            sentences = row.Split(MyGlobal.separator);
            if (!dictPos.ContainsKey(activeBody))
                dictPos.Add(activeBody, Convert.ToInt32(sentences[(int)DialogIndex.initialSentence]));

            //if (zero + dictPos[activeBody] == Convert.ToInt32(sentences[(int)DialogIndex.finalSantence]) + zero || zero + dictPos[activeBody] == sentences.Length)
            //{   //qui è già fuori dictPos[activeBody]izione
            //    dictPos[activeBody] = Convert.ToInt32(sentences[(int)DialogIndex.repeatingNodeStart]);
            //}
            //dialogo.text = sentences[zero + dictPos[activeBody]++];
            //List<string> ls = new List<string>();
            //for(int i = 0; i <= Convert.ToInt32(sentences[(int)DialogIndex.finalSantence]) + zero; i++)
            //    ls.Add(sentences[zero + dictPos[activeBody]++]);
            StartCoroutine(Stampa());
            
        }
    }

    IEnumerator Stampa()
    {
        //    foreach(string s in senteces)
        //    {
        //        dialogo.text = s;
        //        if (dictPos[activeBody] % 2 == 0)
        //        {
        //            tizio_che_parla.text = activeBody;
        //        }
        //        else
        //        {
        //            tizio_che_parla.text = gameObject.name;
        //        }
        //        yield return new WaitForSeconds(0.5f);
        //        dialogo.text = null;
        //        tizio_che_parla.text = null;
        //    }
        int n = Convert.ToInt16(sentences[(int)DialogIndex.finalSantence]) - Convert.ToInt16(sentences[(int)DialogIndex.initialSentence]);
        if (zero + dictPos[activeBody] == Convert.ToInt32(sentences[(int)DialogIndex.finalSantence]) + zero || zero + dictPos[activeBody] == sentences.Length)
        {   //qui è già fuori dictPos[activeBody]izione
            dictPos[activeBody] = Convert.ToInt32(sentences[(int)DialogIndex.repeatingNodeStart]);
            n = Convert.ToInt16(sentences[(int)DialogIndex.finalSantence]) - Convert.ToInt16(sentences[(int)DialogIndex.repeatingNodeStart]);
        }
        for (int i = 0; i < n; i++)
        {
            if (dictPos[activeBody] % 2 == 0)
            {
                tizio_che_parla.text = activeBody;
            }
            else
            {
                tizio_che_parla.text = gameObject.name;
            }
            dialogo.text = sentences[zero + dictPos[activeBody]++];
            yield return new WaitForSeconds(0.5f);
        }
        dialogo.text = null;
        tizio_che_parla.text = null;
    }

}
