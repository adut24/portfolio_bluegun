using System.Collections;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int bombDamage;

    private void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            gameObject.GetComponent<Animator>().enabled = true;
    }

    private void Explode()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
        Collider2D[] objectsPresent = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        transform.localScale = new Vector2(4, 4);

        foreach (Collider2D element in objectsPresent)
        {
            if (element.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(element.GetComponent<Enemy>().ShowDamage());
                element.GetComponent<Enemy>().TakeDamage(bombDamage);
            }
            else if (element.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(bombDamage);
            }
        }
    }

    private void AfterExplosion()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
