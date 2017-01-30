using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

public enum GameState { UsePower,NoPower,OnlyIdea }

public class GameManager : MonoBehaviour {
    public GameState playerState; 
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

    public bool[] flowGameArray;
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
	public bool blockMovement;
	public bool cantUsePower = false;

    // Use this for initialization
    void Start () {
		canvasUI = FindObjectOfType<UI> ().gameObject;
		playerBody = GameObject.FindGameObjectWithTag ("Player");
		cameraRig = GameObject.FindGameObjectWithTag ("CameraRig");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
        MyGlobal.myBody = GameObject.FindGameObjectWithTag("Player");
        /*
		if (cantUsePower) {
			HandlePowerActivation (true);
		} else {
			HandlePowerActivation (false);
		}*/

        switch (playerState)
        {   
            case GameState.UsePower:
                HandlePowerActivation(false);
                break;
            case GameState.NoPower:
                HandlePowerActivation(true);                
                break;
            case GameState.OnlyIdea:
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		//Quando il potere si esaurisce rimandiamo il giocatore nel suo corpo automaticamente
		if (powerQuantity <= 1 && outOfYourBody) {
			outOfYourBody = false;
			GameObject.FindGameObjectWithTag("Player").GetComponent<State_ControlBody> ().ReturnToYourBody ();
		}

        /*switch (playerState)
        {
            case GameState.UsePower:
                HandlePowerActivation(false);
                break;
            case GameState.NoPower:
                HandlePowerActivation(true);
                break;
            case GameState.OnlyIdea:
                break;
            default:
                break;
        }*/
    }


	public void HandlePowerActivation (bool act) {
		if (act) {
			cantUsePower = true;
			cameraRig.GetComponent<FreeLookCam> ().cantUsePower = true;
		} else {
			cantUsePower = false;
			cameraRig.GetComponent<FreeLookCam> ().cantUsePower = false;
		}
	}

	public void StopCameraEffect () {
		StartCoroutine(StopCameraEffectCO());
	}

	IEnumerator StopCameraEffectCO () {
		yield return new WaitForSeconds (0.6f);
		Camera.main.GetComponentInParent<CameraFilterPack_TV_VHS_Rewind>().enabled = false;
	}

    public GameState ChangePlayerState
    {
        get { return playerState; }
        set
        {
            playerState = value;
            switch (playerState)
            {
                case GameState.UsePower:
                    Debug.Log("posso usare tutto");
                    HandlePowerActivation(false);
                    break;
                case GameState.NoPower:
                    HandlePowerActivation(true);
                    Debug.Log("non posso usare niente");
                    break;
                case GameState.OnlyIdea:
                    HandlePowerActivation(false);
                    Debug.Log("posso usare solo l'idea");
                    break;
                default:
                    break;
            }
        }
    }

    public void SetPlayerState(int change)
    {
        ChangePlayerState = (GameState)change;
    }


}
