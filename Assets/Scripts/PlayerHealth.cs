using UnityEngine;
using System.Collections;
using static Armor;
using static Armor.armorType_e;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currenthealth;
    [SerializeField] private HealthBar hb;
    [SerializeField] private bool isInvicible = false;
    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private PauseControl pauseController;
    public armorType_e armor = none;

    private void Start()
    {
        currenthealth = maxHealth;
        hb.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(source.clip, 1f);
            currenthealth -= damage;
            hb.SetHealth(currenthealth);
            isInvicible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(InvincibilityDelay());
            if (currenthealth <= 0)
            {
                Time.timeScale = 0f;
                gameOverMenu.SetActive(true);
                pauseController.isPaused = true;
            }
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.15f);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator InvincibilityDelay()
    {
        yield return new WaitForSeconds(1f);
        isInvicible = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().power);
        }
    }
}
