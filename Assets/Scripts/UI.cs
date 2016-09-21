using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Image cursorImage;
    public Image powerImage;

    void Start ()
    {

	}
	

	void Update ()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            cursorImage.gameObject.SetActive(true);

        else
            cursorImage.gameObject.SetActive(false);
    }
}