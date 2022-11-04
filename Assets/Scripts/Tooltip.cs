using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text headerField;
    [SerializeField] private Text contentField;
    [SerializeField] private LayoutElement layout;
   
    public void SetTextWeapon(Weapon content, string header)
    {
        layout.enabled = false;
        headerField.text = header;
        contentField.text = "Damage: " + content.damage + "\n"
                          + "Attack Speed: " + 1 / content.shootDelay + "\n"
                          + "Number: " + content.projectileNumber + "\n"
                          + "Size: " + content.size + "\n"
                          + "Duration: " + content.projectileDuration;
        if (content.effect != "")
            contentField.text += ("\n" + "Effect: " + content.effect);
    }

    public void SetTextArmor(ArmorData armor)
    {
        layout.enabled = true;
        headerField.text = armor.name;
        contentField.text = armor.description + "\n"
                          + "Damage Reduction: " + armor.defense * 100 + "%" + "\n"
                          + "Speed: " + armor.speed * 100 + "%"; 
    }

    public void SetTextArtifact(ArtifactData artifact)
    {
        layout.enabled = true;
        string header = artifact.name;
        for (int i = 0; i < artifact.level; i++)
        {
            header += " ★";
        }
        headerField.text = header;
        
        contentField.text = artifact.description;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
