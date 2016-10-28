using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Singleton Implementation
	protected static GameManager _self;
	public static GameManager Self
	{
		get
		{
			if (_self == null)
				_self = FindObjectOfType(typeof(GameManager)) as GameManager;
			return _self;
		}
	}

    public bool isAiming;

	public GameObject playerBody;
	public GameObject cameraRig;
	public GameObject mainCamera;
	public GameObject canvasUI;

    /*public GameObject UI_PowerBar;
    public GameObject UI_Hack;
    public GameObject UI_Possession;
    public GameObject UI_Power;		
	public GameObject UI_Return;

    public GameObject UI_Memory;

	public GameObject MemoryImageUI;*/

	public float powerQuantity = 100f;
	public bool outOfYourBody = false;
	public int nBodyChanged = 0;
	public bool isShowMemory = false;

    // Use this for initialization
    void Start () {
		canvasUI = FindObjectOfType<UI> ().gameObject;
		playerBody = GameObject.FindGameObjectWithTag ("Player");
		cameraRig = GameObject.FindGameObjectWithTag ("CameraRig");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
        MyGlobal.myBody = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//Quando il potere si esaurisce rimandiamo il giocatore nel suo corpo automaticamente
		if (powerQuantity <= 1 && outOfYourBody) {
			outOfYourBody = false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<PowerController> ().ReturnToYourBody ();
		}
	}
}
