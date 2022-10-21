using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [System.Serializable]
    public enum armorType_e
    {
        none,
        energyShield,
        fixedArmor,
        lightArmor
    }

    public armorType_e armorType  = armorType_e.energyShield;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
