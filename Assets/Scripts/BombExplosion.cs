using System.Collections;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public int bombDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    private void Explode()
    {
        Collider2D[] objectsPresent = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);
        gameObject.GetComponent<CircleCollider2D>().radius = 2f;
        foreach (Collider2D element in objectsPresent)
        {
            if (element.gameObject.CompareTag("Enemy"))
            {
                StartCoroutine(EnemyTouched(element.gameObject));
            }
            else if (element.gameObject.CompareTag("Player"))
            {
                
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
