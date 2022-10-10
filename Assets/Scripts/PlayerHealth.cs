using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currenthealth;
    [SerializeField] private HealthBar hb;

    void Start()
    {
        currenthealth = maxHealth;
        hb.SetMaxHealth(maxHealth);
    }

    void TakeDamage(int damage)
    {
        currenthealth -= damage;
        hb.SetHealth(currenthealth);
    }
}
