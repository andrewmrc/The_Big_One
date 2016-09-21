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

	public GameObject playerBody;
	public GameObject UI_Possession;

	// Use this for initialization
	void Start () {
		playerBody = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
