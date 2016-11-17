using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


[Serializable]
public class ArrayBool
{
	public string SequenceName;
    public bool[] sequence;
	public GameObject call;
	void Start(){
		call.GetComponent<GameEvents> ().ExecuteNTimes (1);
	}
    
}
public class FlowManager : MonoBehaviour
{

   
    protected static FlowManager _self;
    public static FlowManager Self
    {
        get
        {
            if (_self == null)
                _self = FindObjectOfType(typeof(FlowManager)) as FlowManager;
            return _self;
        }
    }
    public List<ArrayBool> flowGameArray;


    void Start()
    {
		
    }




}
