using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagItem : MonoBehaviour {
	public RatingTag _ratingTag;
	public Text _TagLabelText;
	public Slider _AccordanceSlider;

	public void SetTagText(string text)
	{
		_TagLabelText.text = text;
	}

	public void SetAccordance(float accordance)
	{
		_AccordanceSlider.value = accordance;
	}


	public void ChangeAccordance(float accordance)
	{
		_ratingTag.ChangeAccordance (_TagLabelText.text, accordance);
	}

	public void DeleteTag()
	{
		_ratingTag.DeleteTag (_TagLabelText.text);
	}

}
