using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

public class MenuController : MonoBehaviour {

    public GameObject ResumeUI, PanelExit, PanelMain;


    void Start ()
    {

    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "TestAngelo")
            {
                PanelExit.SetActive(true);
                Time.timeScale = 0;
            }

            else if (SceneManager.GetActiveScene().name == "Main Menu")
            {
                PanelMain.SetActive(true);
            }

        }
    }

    public void Exit()
    {
		Application.Quit ();
        //SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        PanelExit.SetActive(false);
        Time.timeScale = 1;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
