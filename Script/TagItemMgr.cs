using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagItemMgr : MonoBehaviour {

	public Transform _TFParent;
	public GameObject _TagItemPrefab;
	public GameObject _NewTagInputPrefab;

	public List<GameObject> _TagItems = new List<GameObject>();

	public void Init(List<RatingTag.TagAccordance> tags)
	{
		DestroyTagItems ();

		if (tags == null) {
			return;
		}

		foreach (var ta in tags) {
			string tagText = ta._Tag;
			float accordance = ta._Accordance;	
			GameObject newTagItem = GameObject.Instantiate (_TagItemPrefab);
			newTagItem.transform.SetParent (_TFParent);
			TagItem tagItem = newTagItem.GetComponent<TagItem> ();
			tagItem.SetTagText (tagText);
			tagItem.SetAccordance (accordance);
			_TagItems.Add (newTagItem);
		}

		GameObject newTagInputObj = GameObject.Instantiate (_NewTagInputPrefab);
		newTagInputObj.transform.SetParent (_TFParent);
		_TagItems.Add (newTagInputObj);
	}

	public void DestroyTagItems()
	{
		foreach (GameObject obj in _TagItems) {
			Destroy (obj);
		}
		_TagItems.Clear ();
	}

}
