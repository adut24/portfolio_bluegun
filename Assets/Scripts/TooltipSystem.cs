using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;
    [SerializeField] private Tooltip tooltip;

    private void Awake()
    {
        instance = this;
    }

    public void ShowWeapon(Weapon content, string header)
    {
        tooltip.SetTextWeapon(content, header);
        tooltip.gameObject.SetActive(true);
    }

    public void ShowArmor(string name, string desc, float defense, float speed)
    {
        tooltip.SetTextArmor(name, desc, defense, speed);
        tooltip.gameObject.SetActive(true);
    }

    public void ShowArtifact(string content, string header)
    {
        tooltip.SetTextArtifact(content, header);
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}
