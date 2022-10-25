using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicArmor : Armor
{
    public Weapon.dotEffect dot;
    private Debuff debuff;
    public override void Add()
    {
        armor.name = "Garlic Shield";
        armor.description = "Blocks an attack. Recharges after "
            + armor.energyShieldCooldown + " seconds without taking damage.";
    }

    public override void Remove()
    {
        armor.energyShield = false;
        if (armor.armorCoroutine != null)
            StopCoroutine(armor.armorCoroutine);
        Destroy(gameObject);
    }

    private void applyDebuff(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (debuff == null || debuff.isDestroyed == true)
            {
                collision.gameObject.GetComponent<SpriteRenderer>().color = dot.color;
                debuff = Instantiate(dot.debuffPrefab, collision.gameObject.transform);
                debuff.dot = dot;
                debuff.enemy = collision.gameObject.GetComponent<Enemy>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        applyDebuff(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        applyDebuff(collision);
    }

}
