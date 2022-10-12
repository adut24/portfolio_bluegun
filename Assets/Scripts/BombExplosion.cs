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
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    private void Explode()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
        Collider2D[] objectsPresent = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        gameObject.transform.localScale = new Vector2(7, 7);
        gameObject.GetComponent<CircleCollider2D>().radius = 0.7f;
        foreach (Collider2D element in objectsPresent)
        {
            if (element.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(EnemyTouched(element.gameObject));
            }
            else if (element.gameObject.CompareTag("Player"))
            {
                playerHealth.TakeDamage(bombDamage);
            }
        }
    }

    private IEnumerator EnemyTouched(GameObject enemy)
    {
        enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(enemy.GetComponent<Enemy>().ShowDamage());
        enemy.GetComponent<Enemy>().TakeDamage(bombDamage);
        yield return new WaitForSeconds(0.5f);
        if (enemy)
            enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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
