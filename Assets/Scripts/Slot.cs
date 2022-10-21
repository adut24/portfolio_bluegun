using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData weapon;
    public Image weaponVisual;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weapon)
            TooltipSystem.instance.Show(weapon.weaponPrefab.GetComponent<Weapon>(), weapon.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }
}
