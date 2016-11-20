using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
/*
 * in csv:
 * colonna 0 = tizio A, 1 = tizio B
 * pos pari parla A->B, dispari B->A (name)
 * usare '&' per più frasi dallo stesso "name"
 * */
public class Speaking : MonoBehaviour
{
    const int zero = 3;
    private int pos;
    private Dictionary<string, int> dictPos = new Dictionary<string, int>();
    private string activeBody;
    public string[] sentences;
    //prima frase da dire; ultima frase da dire; frase cappio attivata da final; fase serve per distinguere i dialoghi A == A
    public int initialSentence, finalSantence, repeatingNodeStart, fase;
    //dialogo = zona dove sarà visualizzato; name = nome di chi sta parlando
    public Text dialogo, tizio_che_parla;

    void Awake()
    {
        activeBody = null;
    }

    public void Speak()
    {
        if(activeBody != GameObject.FindGameObjectWithTag("Player").name)
        {
            activeBody = GameObject.FindGameObjectWithTag("Player").name;
            dictPos.Add(activeBody, initialSentence);
        }
        string row = CsvFileReader.ReadDialogue(MyGlobal.pathDialogue, activeBody, gameObject.name, fase.ToString());
        if (row != null)
        {
            sentences = row.Split(MyGlobal.separator);
            if(zero + dictPos[activeBody] == finalSantence + zero || zero + dictPos[activeBody] == sentences.Length)
            {   //qui è già fuori dictPos[activeBody]izione
                dictPos[activeBody] = repeatingNodeStart;
            }
            dialogo.text = sentences[zero + dictPos[activeBody]++];
            if (dictPos[activeBody] % 2 == 0)
            {
                tizio_che_parla.text = activeBody;
            }
            else
            {
                tizio_che_parla.text = gameObject.name;
            }
        }
    }

}
