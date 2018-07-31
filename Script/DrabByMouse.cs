using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrabByMouse : MonoBehaviour {
	public UnityEvent _Drag;
	private bool _bDragging = false;
	private Vector2 _MousePosAtBegin;
	private Vector3 _PositionAtBegin;
	public void OnBeginDrag()
	{
		_MousePosAtBegin = Input.mousePosition;
		_PositionAtBegin = transform.position;
		_Drag.Invoke ();
		_bDragging = true;
	}

	public void OnDrag()
	{
		if (!_bDragging)
			return;
		Vector2 mPos = Input.mousePosition;
		Vector2 offset = mPos - _MousePosAtBegin;
		Vector3 posNow = _PositionAtBegin + (Vector3)offset;
		transform.position = posNow;
		_Drag.Invoke ();
	}

	public void OnEndDrag()
	{
		if (!_bDragging)
			return;
		_Drag.Invoke ();
	}

	[ContextMenu("ResetPositionToBeginPos")]
	public void ResetPositionToBeginPos()
	{
		transform.position = _PositionAtBegin;
		_bDragging = false;
	}
}
