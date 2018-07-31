using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(RawImage))]
public class SendRawImageRT : MonoBehaviour {

	[System.Serializable]
	public class EventWithTexture : UnityEvent<Texture> { };
	public EventWithTexture _SendRawImageTexture;

	[ContextMenu("SendRawImageTexture")]
	public void SendRawImageTexture()
	{
		RawImage rImage = GetComponent<RawImage> ();
		_SendRawImageTexture.Invoke (rImage.texture);
	}
}
