using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text headerField;
    [SerializeField] private Text contentField;
   
    public void SetText(Weapon content, string header = "")
    {
        if (header == "")
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = "Damage: " + content.damage + "\n"
                          + "Attack Speed: " + 1 / content.shootDelay + "\n"
                          + "Number: " + content.projectileNumber + "\n"
                          + "Size: " + content.size + "\n"
                          + "Duration: " + content.projectileDuration;
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
