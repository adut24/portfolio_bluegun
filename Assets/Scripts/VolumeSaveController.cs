using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSaveController : MonoBehaviour
{

	[SerializeField] private Slider volumeSlider = null;
	
	[SerializeField] private TextMeshProUGUI volumeTextUI = null;
	
	

	private void Start()
	{
		LoadValues();
	}

	
	public void VolumeSlider(int volume)
	{
		int volumeValue = (int) volumeSlider.value;
		// UnityEngine.Debug.Log("value:");
		// UnityEngine.Debug.Log(volumeSlider.value);
		PlayerPrefs.SetInt("VolumeValue", volumeValue);
		volumeTextUI.text = volumeValue.ToString();
	}
	
	void	LoadValues()
	{
		float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
		UnityEngine.Debug.Log(volumeValue);
		volumeSlider.value = volumeValue;
		AudioListener.volume = volumeValue;
	}

}
