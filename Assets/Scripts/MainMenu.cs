using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public KeepOnLoad script = null;
    public void ChangeScene (string sceneName)
    {
        if (script)
        {
            foreach (GameObject element in script.objects)
            {
                Destroy(element);
            }
        }
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame ()
    {
    	Debug.Log("Quit!");
    	Application.Quit();
    }
}
