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

    public void SetTextArmor(string name, string desc, float defense, float speed)
    {
        layout.enabled = true;
        headerField.text = name;
        contentField.text = desc + "\n"
                          + "Damage Reduction: " + defense * 100 + "%" + "\n"
                          + "Speed: " + speed * 100 + "%"; 
    }

    public void SetTextArtifact(string content, string header)
    {
        layout.enabled = true;
        headerField.text = header;
        contentField.text = content;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
