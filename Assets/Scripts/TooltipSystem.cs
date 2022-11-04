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

    public void ShowArmor(ArmorData armor)
    {
        tooltip.SetTextArmor(armor);
        tooltip.gameObject.SetActive(true);
    }

    public void ShowArtifact(ArtifactData artifact)
    {
        tooltip.SetTextArtifact(artifact);
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}
