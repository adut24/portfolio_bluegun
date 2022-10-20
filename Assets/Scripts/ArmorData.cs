using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Armors/New armor")]

public class ArmorData : ScriptableObject
{
    public new string name;
    public string description;
    public int defense;
    public float speed;
    public Sprite visual;
}
