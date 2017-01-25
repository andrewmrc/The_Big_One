﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;

public class MenuController : MonoBehaviour
{

    public GameObject ResumeButton, PanelExit, panelSettings;

    public CanvasController refCanvasController;

    public bool isInExitMenu;



    private void Start()
    {
        refCanvasController = FindObjectOfType<CanvasController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isInExitMenu)
        {
            Time.timeScale = 0;
            PanelExit.SetActive(true);
            isInExitMenu = true;
            refCanvasController.ExitHandler(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isInExitMenu)
        {
            panelSettings.SetActive(false);
            Resume();
            refCanvasController.ExitHandler(true);

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
        Time.timeScale = 1;
        PanelExit.SetActive(false);
        isInExitMenu = false;
    }
}
