using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyNumber = 0;

    Enemy()
    {
        enemyNumber++;
    }

    ~Enemy()
    {
        enemyNumber--;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Destroy(GameObject.Find("Dummy"));
        }
    }
}
