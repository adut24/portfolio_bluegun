using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveController : MonoBehaviour
{

	[SerializeField] private Slider volumeSlider = null;
	
	[SerializeField] private Text volumeTextUI = null;
	
	

	private void Start()
	{
		LoadValues();
	}

	
	public void VolumeSlider(int volume)
	{
		volumeTextUI.text = volume.ToString();
	}
	
	public void SaveVolumeButton()
	{
		int volumeValue = int(volumeSlider.value);
		PlayerPrefs.SetInt("VolumeValue", volumeValue);
		LoadValues();
	}

	void	LoadValues()
	{
		float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
		volumeSlider.value = volumeValue;
		AudioListener.volume = volumeValue;
	}

}
