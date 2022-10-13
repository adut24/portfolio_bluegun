using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public static int enemyNumber = 0;
    public int health;
    public int power;
    public float moveSpeed;
    public float minDistance;
    public float detectionZone;
    public float maxFollowDistance;
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
        if (!player)
            StartCoroutine(RandomMove());
        if (!player || Vector2.Distance(transform.position, player.transform.position) > minDistance)
            Pathfinding();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(ShowDamage());
            TakeDamage(10);
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
            Collider2D[] detectZone = Physics2D.OverlapCircleAll(transform.position, detectionZone);

            foreach (Collider2D element in detectZone)
            {
                if (element.gameObject.CompareTag("Player"))
                {
                    player = element.gameObject;
                    break;
                }
            }

            if (player)
            {
                Collider2D[] detectCollider = Physics2D.OverlapAreaAll(transform.position, player.transform.position);
                foreach (Collider2D item in detectCollider)
                {
                    if (item.gameObject.name == "Walls")
                    {
                        player = null;
                        break;
                    }
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);

            if (transform.rotation != Quaternion.identity)
                transform.rotation = Quaternion.identity;

            if (Vector2.Distance(transform.position, player.transform.position) > maxFollowDistance)
                player = null;
        }
    }

    private IEnumerator RandomMove()
    {
        Vector2 direction = new Vector2(Random.Range(-100, 100), Random.Range(-10, 100));
        float force = 10000f;

        if (transform.rotation != Quaternion.identity)
            transform.rotation = Quaternion.identity;

        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        yield return new WaitForSeconds(2f);
    }
}
