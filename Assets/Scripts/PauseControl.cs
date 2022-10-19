using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
	public bool isPaused = false;
	public GameObject menu;
    // Update is called once per frame
    public void SetPause(bool pause)
    {
        	if (pause is true)
        	{
            	Time.timeScale = 0f;
				menu.SetActive(true);
				isPaused = true;
        	}
        	else
        	{
        		Time.timeScale = 1;
				menu.SetActive(false);
				isPaused = false;
        	}
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SetPause(!isPaused);
        }
    }
}
