using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_Enable : MonoBehaviour {

	public UnityEvent _Enable;

	public void OnEnable()
	{
		_Enable.Invoke ();
	}

}
