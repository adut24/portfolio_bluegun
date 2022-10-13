using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currenthealth;
    [SerializeField] private HealthBar hb;

    private void Start()
    {
        currenthealth = maxHealth;
        hb.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        hb.SetHealth(currenthealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().power);
        }
    }
}
