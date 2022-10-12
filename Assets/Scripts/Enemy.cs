using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static int enemyNumber = 0;
    public int health;
    public int power;
    public float moveSpeed;
    public float maxDistance;
    private GameObject player;

    Enemy()
    {
        enemyNumber++;
    }

    ~Enemy()
    {
        enemyNumber--;
    }

    private void FixedUpdate()
    {
        if (!player || Vector2.Distance(transform.position, player.transform.position) > maxDistance)
            Pathfinding();
        else
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    private void Pathfinding()
    {
        if (!player)
        {
            Collider2D[] detectZone = Physics2D.OverlapCircleAll(gameObject.transform.position, 10f);

            foreach (Collider2D element in detectZone)
            {
                if (element.gameObject.CompareTag("Player"))
                {
                    player = element.gameObject;
                    break;
                }
            }
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
        }
    }
}
