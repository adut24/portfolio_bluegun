using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Armors/New armor")]

public class ArmorData : ScriptableObject
{
    public new string name;
    public Sprite visual;
}
