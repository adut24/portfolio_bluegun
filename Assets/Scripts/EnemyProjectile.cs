using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    public float speedForce;
    public int power;
    private Vector3 direction;

    private void Start()
    {
        player = GameObject.Find("Player");
        direction = player.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        transform.position += speedForce * Time.deltaTime * direction;
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
