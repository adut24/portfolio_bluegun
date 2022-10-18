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
        direction = transform.position;
        direction = player.transform.position - direction;
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
