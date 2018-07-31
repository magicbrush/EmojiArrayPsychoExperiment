using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleToSlider : MonoBehaviour {
	public float _OnValue = 1.0f;
	public float _OffValue = -1.0f;
	public Slider _slider;

	public void Turn(bool bON)
	{
		if (bON) {
			_slider.value = _OnValue;
		} else {
			_slider.value = _OffValue;
		}
	}
}
