using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/New weapon")]

public class WeaponData : ScriptableObject
{
    public new string name;
    public Sprite visual;
    public GameObject weaponPrefab;
}
