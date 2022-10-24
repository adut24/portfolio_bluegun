using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArmor : Armor
{
    [SerializeField]
    private PlayerHealth ph;
    private PlayerMovement pm;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void Add()
    {
        pm.speedModifier += this.armor.speed;
        ph.armorValue += this.armor.defense;
    }

    public override void Remove()
    {
        pm.speedModifier += this.armor.speed;
        ph.armorValue += this.armor.defense;
    }
}
