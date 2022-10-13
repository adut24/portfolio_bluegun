using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
	private bool isPaused = false;
	public GameObject pauseMenuUI;
    // Update is called once per frame
    public void SetPause(bool pause)
    {
        	if (!pause)
        	{
        		UnityEngine.Debug.Log("it wejgiwejgwe?");
        		Time.timeScale = 0f;
				pauseMenuUI.SetActive(true);
				isPaused = true;
        	}
        	else
        	{
        		UnityEngine.Debug.Log("hello?");
        		Time.timeScale = 1;
				pauseMenuUI.SetActive(false);
				isPaused = false;
        	}
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SetPause(isPaused);
        }
    }
}
