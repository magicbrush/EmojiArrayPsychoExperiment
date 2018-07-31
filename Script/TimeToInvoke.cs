using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeToInvoke : MonoBehaviour {

	public UnityEvent _Event;
	public float _defaultTime = 1.0f;

	[ContextMenu("GetReadyToInvoke")]
	public void GetReadyToInvoke()
	{
		GetReadyToInvoke (_defaultTime);
	}

	public void GetReadyToInvoke(float time)
	{
		Invoke("InvokeEvent", time);
	}
		
	private void InvokeEvent()
	{
		_Event.Invoke ();
	}

}
