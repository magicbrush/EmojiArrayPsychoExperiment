using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_Start : MonoBehaviour {

	public UnityEvent _Start;

	// Use this for initialization
	void Start () {
		_Start.Invoke ();
	}

}
