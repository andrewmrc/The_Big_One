using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

public class MenuController : MonoBehaviour
{

    public GameObject ResumeButton, ExitButton, PanelExit, panelSettings;

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
        if (!isInExitMenu)
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") && !isInExitMenu)
        {
			renderCamera.SetActive(true);
            isInExitMenu = true;
  
			Debug.Log ("PAUSA");

            ResumeButton.GetComponent<Button> ().Select ();
            
            refCanvasController.ExitHandler(false);

			PanelExit.SetActive(true);
			Time.timeScale = 0;  
        }
		else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") && isInExitMenu)
        {
			Debug.Log ("SELECT");
			ExitButton.GetComponent<Button> ().Select();
            Resume();
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
        
		Debug.Log ("RESUME");
        Time.timeScale = 1;
        PanelExit.SetActive(false);
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
