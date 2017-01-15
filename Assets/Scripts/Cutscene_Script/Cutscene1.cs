using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cutscene1 : MonoBehaviour {

	public void SkipBtn(string Skip)
    {
        SceneManager.LoadScene(Skip);
    }
}
