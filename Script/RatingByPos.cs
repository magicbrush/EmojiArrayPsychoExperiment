using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RatingByPos : MonoBehaviour {
	public Transform _OriginTF;
	public Transform _RightTF;
	public Transform _UpTF;

	public Bounds _Bounds;
	public UnityEvent _Exceed;

	private float _Exitement = float.NegativeInfinity;
	private float _Pleasure = float.NegativeInfinity;

	[System.Serializable]
	public class EventWithString: UnityEvent<string>{};
	public EventWithString _RateValues;

	public float GetExitement()
	{
		return _Exitement;
	}

	public float GetPleasure()
	{
		return _Pleasure;
	}

	[ContextMenu("RateValue")]
	public void RateValue()
	{
		Vector3 posOrigin = _OriginTF.transform.position;
		Vector3 posRight = _RightTF.transform.position;
		Vector3 posUp = _UpTF.transform.position;

		Vector3 Right = posRight - posOrigin;
		Vector3 Up = posUp - posOrigin;

		Vector3 VecNow = transform.position - _OriginTF.position;

		_Exitement = 100.0f*Vector3.Dot (VecNow, Right.normalized)/Right.magnitude;
		_Pleasure = 100.0f*Vector3.Dot (VecNow, Up.normalized)/Up.magnitude;

		Vector3 value2 = new Vector3 (_Exitement, _Pleasure,0.0f);
		bool bValid = _Bounds.Contains (value2);
		if (!bValid) {
			_Exceed.Invoke ();
		}

		string text = "(" + _Exitement.ToString ("F0") + "," + _Pleasure.ToString ("F0") + ")";
		_RateValues.Invoke (text);
		//print ("Exitement:" + Exitement.ToString () + " Pleasure:" + Pleasure.ToString());
	}
}
