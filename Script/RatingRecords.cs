using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using UnityEngine.UI;

public class RatingRecords : MonoBehaviour {

	[System.Serializable]
	public struct Rating
	{
		public string _Name;
		public float _Exitement;
		public float _Pleasure;
	}

	public Transform _BtnTFParent;
	public string _FileName;
	public List<Rating> _Ratings = new List<Rating> ();
	public RatingByPos[] ratings;

	public void Start()
	{
		GetRatings ();
	}

	public void SetFileNamePrefix(string pref)
	{
		_FileName = pref;
	}

	[ContextMenu("GetRatings")]
	public void GetRatings()
	{
		ratings = _BtnTFParent.GetComponentsInChildren<RatingByPos> ();
	}

	[ContextMenu("GetRecords")]
	public void GetRecords()
	{
		_Ratings.Clear ();
		//RatingByPos[] ratings = _BtnTFParent.GetComponentsInChildren<RatingByPos> ();
		foreach (var r in ratings) {
			Rating rating = new Rating ();
			rating._Exitement = r.GetExitement ();
			rating._Pleasure = r.GetPleasure ();
			VideoPlayer vp = r.GetComponentInChildren<VideoPlayer> ();
			if (vp) {
				rating._Name = vp.clip.name;
			} else {
				RawImage rimg = r.GetComponentInChildren<RawImage> ();
				rating._Name = rimg.texture.name;
			}

			_Ratings.Add (rating);
		}
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
		string fileUrl = dir + _FileName + timeString2 + ".txt";
		print ("file:" + fileUrl);

		string ratingTexts = 
			ConvertRating2String2 (_Ratings);
		File.WriteAllText (fileUrl, ratingTexts);
		//File.WriteAllText(fileUrl,

		//File.WriteAllText(
	}

	static public string ConvertRating2String(List<Rating> ratings)
	{
		string text = "";
		for (int i = 0; i < ratings.Count; i++) {
			string itemText = "";
			itemText+= ratings [i]._Name;
			itemText += " Exitement: " + ratings [i]._Exitement.ToString ();
			itemText += " Pleasure: " + ratings [i]._Pleasure.ToString ();
			//itemText +
			itemText += "\n";
			text += itemText;
		}
		return text;
	}

	static public string ConvertRating2String2(List<Rating> ratings)
	{
		string text = "";

		text += "Video/Image Names:\n";
		for (int i = 0; i < ratings.Count; i++) {
			string itemText = "";
			itemText += ratings [i]._Name;
			itemText += "\n";
			text += itemText;
		}
		text += "\n";
		text += "Exitement:\n";
		for (int i = 0; i < ratings.Count; i++) {
			string itemText = "";
			itemText += ratings [i]._Exitement.ToString();
			itemText += "\n";
			text += itemText;
		}
		text += "\n";
		text += "Pleasure:\n";
		for (int i = 0; i < ratings.Count; i++) {
			string itemText = "";
			itemText += ratings [i]._Pleasure.ToString();
			itemText += "\n";
			text += itemText;
		}
		return text;
	}

}
