using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int health;
    public int power;
    public float baseSpeed;
    public float moveSpeed;
    public float minDistance;
    public float detectionZone;
    public float maxFollowDistance;
    public string walkAnim;
    public int dropRate;
    public int healPower;
    protected GameObject player;
    protected SpriteRenderer sprite;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected float execTime = 2f;
    protected Vector2 dir;
    private bool _alive = true;
    private Color _nextColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    public bool alive
    {
        get {return _alive;}
    }

    public Color nextColor
    {
        get {return _nextColor;}
        set {_nextColor = value;}
    }

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        baseSpeed = moveSpeed;
    }

    protected virtual void Update()
    {
        execTime -= Time.deltaTime;
        if (!player && execTime <= 0)
            dir = new Vector2(Random.Range(-1000, 1000), Random.Range(-1000, 1000));
    }

    protected virtual void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Introduction" && !player && execTime <= 0)
            MoveRandom();
        if (!player || Vector2.Distance(transform.position, player.transform.position) > minDistance)
            Pathfinding();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(ShowDamage());
            TakeDamage(collision.gameObject.GetComponent<Projectile>().parent.damage);
        }
    }

    public IEnumerator ShowDamage()
    {
        Color previousColor;
        if (_alive == true)
        {
            previousColor = sprite.color;
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            if (this)
            {
                sprite.color = nextColor;
                nextColor = Color.white;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (_alive == true && health <= 0)
        {
            _alive = false;
            AudioSource source = GetComponent<AudioSource>();
            rb.bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;
            if (source != null)
                source.PlayOneShot(source.clip, 1f);
            Vector2 position = gameObject.transform.position;
            Destroy(gameObject, 1.0f);
            if (SceneManager.GetActiveScene().name != "Introduction")
                DropItem(position);
        }
    }

    protected virtual void Pathfinding()
    {
        if (_alive == true)
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
                if (anim != null && walkAnim != null)
                    anim.Play(walkAnim);
                Flip(transform.position, player.transform.position);
                if (Vector2.Distance(transform.position, player.transform.position) > maxFollowDistance)
                    player = null;
            }
        }
    }

    protected virtual void MoveRandom()
    {
        if (_alive == true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(dir * 1.25f);
            if (anim != null && walkAnim != null)
                anim.Play(walkAnim);
            Flip(transform.position, dir);
            execTime = 2f;
        }
    }

    protected virtual void Flip(Vector2 position, Vector2 target)
    {
        if (position.x > target.x)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    protected virtual void DropItem(Vector2 position)
    {
        int nb = Random.Range(1, 100);

        if (nb <= dropRate)
        {
            GameObject potion = Resources.Load<GameObject>("Potion");
            GameObject spawnedPotion = Instantiate(potion, position, Quaternion.identity);
            spawnedPotion.GetComponent<Potion>().healPower = healPower;
        }
    }
}
