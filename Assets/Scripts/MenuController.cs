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


    private void Start()
    {
        refCanvasController = FindObjectOfType<CanvasController>();
        

    }

    void Update()
    {
        if (!isInExitMenu)
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") && !isInExitMenu)
        {
            isInExitMenu = true;
            

            
            //prova = Sprite.Create(gameTex, new Rect(0, 0, gameTex.width, gameTex.height), new Vector2(0.5f, 0.5f));

            PanelExit.GetComponentInChildren<RawImage>().texture = gameTex ;
            
            
			Debug.Log ("PAUSA");
            Time.timeScale = 0;
            PanelExit.SetActive(true);

            Application.CaptureScreenshot("Assets/Resources/menu.png");
            gameTex = Resources.Load("menu") as Texture;
            ResumeButton.GetComponent<Button> ().Select ();
            
            refCanvasController.ExitHandler(false);
            

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
