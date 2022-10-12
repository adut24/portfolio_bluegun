using UnityEngine;

public class KeepOnLoad : MonoBehaviour
{
    public GameObject[] objects;

    private void Awake()
    {
        foreach (GameObject element in objects)
        {
            DontDestroyOnLoad(element);
        }
    }
}
