using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currenthealth;
	public float armorValue = 0.0f;
    [SerializeField] private HealthBar hb;
    [SerializeField] public bool isInvicible = false;
    [SerializeField] private SpriteRenderer graphics;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private PauseControl pauseController;
    private GameObject armor;

    private void Start()
    {
        currenthealth = maxHealth;
        hb.SetMaxHealth(maxHealth);
        hb.SetHealth(maxHealth);
    }


    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            armor = GameObject.FindWithTag("Armor");
            if (armor != null)
            {
                ArmorData armorData = armor.GetComponent<Armor>().armor;
                if (armorData.energyShield == true)
                {
                    if (armorData.armorCoroutine != null)
                        StopCoroutine(armorData.armorCoroutine);
                    armorData.armorCoroutine = StartCoroutine(armor.GetComponent<EnergyArmor>().EnergyShieldDown());
                    isInvicible = true;
                    StartCoroutine(InvincibilityDelay());
                    return;
                }
            }
            AudioSource source = GetComponent<AudioSource>();
            source.PlayOneShot(source.clip, 1f);
            currenthealth -= Mathf.RoundToInt ((float) damage * (1.0f - armorValue));
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Knight") &&
            collision.gameObject.transform.Find("Slash").gameObject.activeSelf)
            TakeDamage(collision.gameObject.GetComponent<Knight>().slashPower);
    }

    public void RegenerateLife(int heal)
    {
        if (currenthealth + heal > maxHealth)
            currenthealth = maxHealth;
        else
            currenthealth += heal;
        hb.SetHealth(currenthealth);
    }
}
