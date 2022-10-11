using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyNumber = 0;
    public int health;

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
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(ShowDamage());
        }
    }

    public IEnumerator ShowDamage()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.3f);
        if (this)
            this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
