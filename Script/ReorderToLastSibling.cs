using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorderToLastSibling : MonoBehaviour {

	public void Reorder()
	{
		transform.SetAsLastSibling ();
	}
}
