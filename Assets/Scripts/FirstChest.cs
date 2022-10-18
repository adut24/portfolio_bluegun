using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstChest : MonoBehaviour
{
    [SerializeField] private Inventory inv;
    [SerializeField] private Transform invPanel;
    [SerializeField] private GameObject weapon;
    private WeaponData[] allWeapons;

    public void Awake()
    {
        allWeapons = Resources.LoadAll<WeaponData>("Weapons/");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int i = Random.Range(0, allWeapons.Length);
            inv.weapon = allWeapons[i];
            invPanel.GetChild(0).GetChild(0).GetComponent<Image>().sprite = allWeapons[i].visual;
            Destroy(gameObject);
            weapon.SetActive(true);
            weapon.GetComponent<SpriteRenderer>().sprite = allWeapons[i].visual;
        }
    }
}
