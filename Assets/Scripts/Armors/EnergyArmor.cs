using UnityEngine;
using System.Collections;

public class EnergyArmor : Armor
{
	public override void Add()
	{
        Debug.Log("this goes here");
		gameObject.GetComponent<SpriteRenderer>().sprite = armor.gameVisual;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
		armor.energyShield = true;
		armor.name = "EnergyShield";
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

	public IEnumerator EnergyShieldDown()
	{
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		armor.energyShield = false;
		yield return new WaitForSeconds(armor.energyShieldCooldown);
		armor.energyShield = true;
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		armor.armorCoroutine = null;
	}
}
