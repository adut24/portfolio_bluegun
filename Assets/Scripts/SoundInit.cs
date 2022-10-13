using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundInit : MonoBehaviour
{
    public AudioMixer mixer;
	public int			startVolume;

    void   Start()
    {
    	float volume = ((float) startVolume) / 100;
		
        mixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);

    }
}
