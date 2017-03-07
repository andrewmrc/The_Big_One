using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;
using System.IO;

public class MenuController : MonoBehaviour
{

    public GameObject ResumeButton, ExitButton, LoadButton, PanelExit, panelSettings;

    public CanvasController refCanvasController;

    public bool isInExitMenu;
    public Texture gameTex;
    public Sprite prova;
	private GameObject renderCamera;

    private void Start()
    {
        refCanvasController = FindObjectOfType<CanvasController>();
		renderCamera = GameObject.FindGameObjectWithTag ("CameraRenderTexture");
		renderCamera.SetActive (false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause") && !isInExitMenu)
        {            
			CheckContinua ();
            renderCamera.SetActive(true);            
            isInExitMenu = true;            
            Debug.Log ("PAUSA");
            refCanvasController.ExitHandler(false);
            PanelExit.SetActive(true);
            ResumeButton.GetComponent<Button>().Select();
            Time.timeScale = 0;

        }
		else if (Input.GetButtonDown("Pause") && isInExitMenu)
        {
			Debug.Log ("SELECT");

            ExitButton.GetComponent<Button>().Select();
            Resume();
        }
    }


	public void CheckContinua () {
		string saveName = Application.persistentDataPath + "/" + 0 + ".txt";
		if (File.Exists (saveName)) {
			LoadButton.GetComponent<Button>().interactable = true;
		} else {
			LoadButton.GetComponent<Button>().interactable = false;
		}
	}


    public void Exit()
    {
        //Application.Quit ();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
		ExitButton.GetComponent<Button> ().Select ();
		Debug.Log ("RESUME");
        Time.timeScale = 1;
        PanelExit.SetActive(false);
		panelSettings.SetActive (false);
		PanelExit.transform.GetChild(0).gameObject.SetActive (true);
		renderCamera.SetActive(false);
        isInExitMenu = false;
        refCanvasController.ExitHandler(true);
    }

    IEnumerator MovingDisplay()
    {
        while (gameTex != null)
        {          
            yield return null;
        }
        
    }
}
