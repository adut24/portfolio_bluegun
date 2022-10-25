using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData weapon;
    public ArmorData armor;
    public ArtifactData artifact;
    public Image visual;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weapon)
            TooltipSystem.instance.ShowWeapon(weapon.weaponPrefab.GetComponent<Weapon>(), weapon.name);
        else if (armor)
            TooltipSystem.instance.ShowArmor(armor.name, armor.description, armor.defense, armor.speed);
        else if (artifact)
            TooltipSystem.instance.ShowArtifact(artifact.description, artifact.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }
}
