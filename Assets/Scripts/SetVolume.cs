using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public TextMeshProUGUI volumeText;

    void   Start()
    {
    	UnityEngine.Debug.Log("Hello");
        volumeText = GameObject.Find("VolumeValue").GetComponent<TextMeshProUGUI>();
        int volValue = (int) MathF.Floor(this.GetComponent<Slider>().value * 100);
        volumeText.text = volValue.ToString();
    }
    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        volumeText.text = (MathF.Floor(sliderValue * 100)).ToString();
    }
}
