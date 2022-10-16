using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public KeepOnLoad script = null;
    public void ChangeScene (string sceneName)
    {
    	UnityEngine.Debug.Log("before");
        if (script)
        {
            UnityEngine.Debug.Log("this should print");
            foreach (GameObject element in script.objects)
            {
                Destroy(element);
            }
        }
        UnityEngine.Debug.Log("after");
        
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame ()
    {
    	Debug.Log("Quit!");
    	Application.Quit();
    }
}
