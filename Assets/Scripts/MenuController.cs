using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

public class MenuController : MonoBehaviour
{

    public GameObject ResumeUI, PanelExit, PanelMain;

    public bool inMenu;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "Main Menu" && !inMenu)
            {
                PanelExit.SetActive(true);
                Time.timeScale = 0;
                inMenu = true;
            }
            else if (inMenu)
            {
                Resume();
            }

            else
            {
                PanelMain.SetActive(true);
            }

        }

    }

    public void Exit()
    {
        //Application.Quit ();
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        PanelExit.SetActive(false);
        Time.timeScale = 1;
        inMenu = false;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}
