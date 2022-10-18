using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    public float speedForce;
    public int power;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(power);
        }
        Destroy(gameObject);
    }
}
