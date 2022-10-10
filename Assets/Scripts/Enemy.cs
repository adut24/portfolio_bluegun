using System.Collections;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ShowDamage());
    }

    private IEnumerator ShowDamage()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
