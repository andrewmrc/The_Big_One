using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject cursor;
    public GameObject UI_Possession;
    public GameObject UI_Power;
    public GameObject UI_Memory;
    public GameObject UI_Return;
    public GameObject UI_Hack;
    public GameObject UI_PowerBar;
    public GameObject memoryImageUI;
    public Text ExaminTextUI;

    PowerController refPC;

	void Start ()
    {
        refPC = FindObjectOfType<PowerController>();
    }
	

	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            cursor.SetActive(true);
        }

        else
        {
            cursor.SetActive(false);
        }
            
    }

    public void PossessionUI(bool on)
    {
        UI_Possession.SetActive(on);
    }

    public void PowerUI(bool on)
    {
        UI_Power.SetActive(on);
    }

    public void MemoryUI(bool on)
    {
        UI_Memory.SetActive(on);
    }

    public void ReturnUI(bool on)
    {
        UI_Return.SetActive(on);
    }

    public void HackUI(bool on)
    {
        UI_Hack.SetActive(on);
    }

    public void PowerBarUI(bool on)
    {
        UI_PowerBar.SetActive(on);
    }

    public void MemoryImageUIHand(bool on)
    {
        memoryImageUI.SetActive(on);
    }

    public void ExamineMemory(Sprite memorySprite, bool on)
    {
        memoryImageUI.GetComponent<Image>().sprite = memorySprite;
        memoryImageUI.SetActive(on);        
    }

    public void ExaminableText(bool on)
    {
        ExaminTextUI.text = "Premi E per esaminare";
        ExaminTextUI.gameObject.SetActive(on);
    }
}
