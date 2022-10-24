using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Armors/New armor")]

public class ArmorData : ScriptableObject
{
    public new string name;
    public string description;
    public float defense;
    public float speed;
    public bool energyShield;
    public float energyShieldCooldown;
    public Coroutine armorCoroutine = null;
    public Sprite visual;
    public Sprite gameVisual;
    public GameObject armorPrefab;
}
