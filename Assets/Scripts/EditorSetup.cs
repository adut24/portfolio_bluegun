using UnityEngine;
using UnityEngine.Audio;

public class EditorSetup : MonoBehaviour
{
    public AudioMixer mixer;

    void Start()
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(0.2f) * 20);
    }
}
