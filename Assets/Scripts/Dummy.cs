using System.Collections;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int health = 100;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            StartCoroutine(ShowDamage());
            TakeDamage(collision.gameObject.GetComponent<Projectile>().parent.damage);
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
            health = 100;
    }
}
