using UnityEngine;
using UnityEngine.EventSystems;

public class KeepOnLoad : MonoBehaviour
{
    public GameObject[] objects;

    private void Awake()
    {
        foreach (GameObject element in objects)
        {
            if (element != null)
                DontDestroyOnLoad(element);
        }
    }
}
