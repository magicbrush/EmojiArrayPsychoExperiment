using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class RatingTag : MonoBehaviour {

	[System.Serializable]
	public struct TagAccordance
	{
		public string _Tag;
		[Range(0,1.0f)]
		public float _Accordance;
	}

	[System.Serializable]
	public class Rating
	{
		public string _Name;
		public GameObject _EmojiShowObj;
		public List<TagAccordance> _Tags = new List<TagAccordance> ();
	}

	public List<Rating> _Ratings = new List<Rating>();
	private Dictionary<GameObject,Rating> _RatingDict = new Dictionary<GameObject, Rating> ();

	public GameObject _EmojiShowParentObj;
	public List<GameObject> _EmojiShowObjs = new List<GameObject>();

	[System.Serializable]
	public class EventWithString: UnityEvent<string>{};
	public EventWithString _ProcedureText;

	[System.Serializable]
	public class EventWithTagAccordanceList: UnityEvent<List<TagAccordance>>{};
	public EventWithTagAccordanceList _TagAccordanceList;

	public void Start()
	{
		InitRatingDict ();
		RateNextEmojiShowObj ();
	}

	public string _FilePrefix = "意向测试";
	public void SetFileName(string filePrefix)
	{
		_FilePrefix = filePrefix;
	}
	[ContextMenu("SaveToFile")]
	public void SaveToFile()
	{
		//string dir = Application.persistentDataPath;
		string dir = "";
		//dir += "/";
		string timeString = System.DateTime.Now.ToFileTime ().ToString ();
		timeString.Replace ("0.", "_");
		int length = timeString.Length;
		string timeString2 = 
			timeString.Substring (length - 11);
		string filePath = dir + _FilePrefix + timeString2 + ".txt";
		print ("filePath:" + filePath);

		string ratingTexts = 
			ConvertRatings2String (_Ratings);
		File.WriteAllText (filePath, ratingTexts);
	}

	private string ConvertRatings2String(List<Rating> ratings)
	{
		string text = "";
		for (int i = 0; i < ratings.Count; i++) {
			string itemText = "";
			itemText +="----------------------------- ";
			itemText += "\n";
			itemText += "Name: ";
			itemText+= ratings [i]._Name;
			itemText += "\n\n";
			itemText += "Tags:\n";
			foreach (var ta in ratings[i]._Tags) {
				itemText += ta._Tag;
				itemText += "\n";
			}
			itemText += "\n";
			itemText += "Accordance:\n";
			foreach (var ta in ratings[i]._Tags) {
				itemText += ta._Accordance;
				itemText += "\n";
			}
			itemText += "\n\n\n";
			text += itemText;
		}
		return text;
	}

	public void ChangeAccordance(string tagName, float accord)
	{
		Rating rating = GetCurrentRating ();
		for (int i = 0; i < rating._Tags.Count; i++) {
			TagAccordance ta = rating._Tags [i];
			if (ta._Tag == tagName) {
				ta._Accordance = accord;
			}
			rating._Tags [i] = ta;
		}
	}

	public void DeleteTag(string tagName)
	{
		Rating rating = GetCurrentRating ();
		for (int i = 0; i < rating._Tags.Count; i++) {
			TagAccordance ta = rating._Tags [i];
			if (ta._Tag == tagName) {
				rating._Tags.RemoveAt (i);
				RateAnotherEmojiShowObj (0);
				return;
			}
		}
	}

	public Rating GetCurrentRating()
	{
		int id = GetCurrentID ();
		return _Ratings [id];
	}

	[ContextMenu("GetEmojiShowObjs")]
	public void GetEmojiShowObjs()
	{
		_EmojiShowObjs.Clear ();
		int cnt = _EmojiShowParentObj.transform.childCount;
		for (int i = 0; i < cnt; i++) {
			GameObject obj = 
				_EmojiShowParentObj.transform.GetChild (i).gameObject;
			_EmojiShowObjs.Add (obj);
		}
	}
		
	[ContextMenu("RatePrevEmojiShowObj")]
	public void RatePrevEmojiShowObj()
	{
		RateAnotherEmojiShowObj (-1);
	}

	[ContextMenu("RateNextEmojiShowObj")]
	public void RateNextEmojiShowObj()
	{
		RateAnotherEmojiShowObj (1);
	}

	public void RateAnotherEmojiShowObj(int inc)
	{
		int curId = GetCurrentID ();
		int cnt = _EmojiShowParentObj.transform.childCount;
		int idNew = curId + inc;
		List<TagAccordance> Tags = null;
		for (int i = 0; i < cnt; i++) {
			GameObject obj = 
				_EmojiShowParentObj.transform.GetChild (i).gameObject;
			if (i == idNew) {
				obj.SetActive (true);

				Rating rating = _RatingDict [obj];
				string name = rating._Name;
				Tags =rating._Tags;
			} else {
				obj.SetActive (false);
			}
		}

		_TagAccordanceList.Invoke (Tags);

		if (idNew >= cnt || idNew < 0) {
			idNew = -1;
		}
		int id = idNew;
		InvokeProcedureText (id);
	}

	void InvokeProcedureText (int id)
	{
		string procedureText = (id + 1).ToString () + " / " + _EmojiShowParentObj.transform.childCount.ToString ();
		_ProcedureText.Invoke (procedureText);
	}
		
	private int GetCurrentID()
	{
		int cnt = _EmojiShowParentObj.transform.childCount;
		int id = -1;
		for (int i = 0; i < cnt; i++) {
			GameObject obj = 
				_EmojiShowParentObj.transform.GetChild (i).gameObject;
			if (obj.activeInHierarchy) {
				id = i;
			}
		}
		return id;
	}

	private GameObject GetCurrentEmojiShowObj()
	{
		int cnt = _EmojiShowParentObj.transform.childCount;
		int id = -1;
		for (int i = 0; i < cnt; i++) {
			GameObject obj = 
				_EmojiShowParentObj.transform.GetChild (i).gameObject;
			if (obj.activeInHierarchy) {
				return obj;
			}
		}
		return null;
	}

	[ContextMenu("InitRatings")]
	public void InitRatings()
	{
		GetEmojiShowObjs ();

		_Ratings.Clear ();
		//_RatingDict.Clear ();
		foreach (var obj in _EmojiShowObjs) {
			RawImage rimg = obj.GetComponentInChildren<RawImage> ();
			CheckInitTexture (rimg);
			string name = rimg.texture.name;
			Rating rating = new Rating ();
			rating._Name = name;
			rating._EmojiShowObj = obj;
			_Ratings.Add (rating);
			//_RatingDict [obj] = rating;
		}
		InitRatingDict ();
		RateAnotherEmojiShowObj (999999);
		//RateNextEmojiShowObj ();
	}

	[ContextMenu("InitRatingDict")]
	public void InitRatingDict()
	{
		_RatingDict.Clear ();
		foreach (var rating in _Ratings) {
			_RatingDict [rating._EmojiShowObj] = rating;
		}
	}



	public void TryAddNewTags(string tagNames)
	{
		string[] tagNameSplits = tagNames.Split (' ');
		foreach (string s in tagNameSplits) {
			print (s + ",");
		}

		List<string> tagNameList = new List<string> ();
		for (int i = 0; i < tagNameSplits.Length; i++) {
			if (tagNameSplits[i].Length ==0 || tagNameSplits [i] [0] == ' ') {
				continue;
			}
			tagNameList.Add (tagNameSplits [i]);
		}

		if (tagNameList.Count == 0) {
			return;
		}

		foreach (string s in tagNameList) {
			TryAddNewTag (s);
		}

		RateAnotherEmojiShowObj (0);
	}

	public void TryAddNewTag(string tag)
	{
		GameObject emojiShowObj = GetCurrentEmojiShowObj ();
		if (emojiShowObj == null) {
			return;
		}
		bool bExist = false;
		foreach (var ta in _RatingDict [emojiShowObj]._Tags) {
			if (ta._Tag == tag) {
				bExist = true;
				break;
			}
		}
		if (bExist) {
			return;
		}

		TagAccordance taNew = new TagAccordance ();
		taNew._Tag = tag;
		taNew._Accordance = 0.75f;

		_RatingDict [emojiShowObj]._Tags.Add (taNew);
	}

	static void CheckInitTexture (RawImage rimg)
	{
		if (rimg.texture == null) {
			VideoDisp vdisp = rimg.GetComponent<VideoDisp> ();
			if (vdisp) {
				vdisp.Init ();
			}
		}
	}


}
