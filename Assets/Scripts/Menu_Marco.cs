using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Marco : MonoBehaviour {

	public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
