using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private float execTime = 2f;
    private Vector2 dir;

    Enemy()
    {
        enemyNumber++;
    }

    ~Enemy()
    {
        enemyNumber--;
    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        execTime -= Time.deltaTime;
        if (!player && execTime <= 0)
            dir = new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Introduction" && !player && execTime <= 0)
            MoveRandom();
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
        sprite.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        if (this)
            sprite.color = Color.white;
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

            foreach (Collider2D obj in detectZone)
            {
                if (obj.gameObject.CompareTag("Player"))
                {
                    player = obj.gameObject;
                    break;
                }
            }

            if (player)
            {
                Collider2D[] detectCollider = Physics2D.OverlapAreaAll(transform.position, player.transform.position);
                foreach (Collider2D obj in detectCollider)
                {
                    if (obj.gameObject.name == "Walls")
                    {
                        player = null;
                        break;
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed);
            Flip(transform.position, player.transform.position);
            if (Vector2.Distance(transform.position, player.transform.position) > maxFollowDistance)
                player = null;
        }
    }

    private void MoveRandom()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(dir * 100000f);
        Flip(transform.position, dir);
        execTime = 2f;
    }

    private void Flip(Vector2 position, Vector2 target)
    {
        if (position.x > target.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
}
