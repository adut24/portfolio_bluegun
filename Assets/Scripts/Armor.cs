using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public ArmorData armor;
    private PlayerHealth ph;
    private PlayerMovement pm;

    private void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Start is called before the first frame update
    public virtual void Add()
    {
        pm.speedModifier *= this.armor.speed;
        ph.armorValue += this.armor.defense;
    }


    // Update is called once per frame
    public virtual void Remove()
    {
        if (pm != null)
            pm.speedModifier /= this.armor.speed;
        if (ph != null)
            ph.armorValue -= this.armor.defense;
    }
}
