using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public WeaponData weapon;
    public ArmorData armor;
    public ArtifactData artifact;

    [SerializeField] private Transform invPanel;
    private WeaponData[] allWeapons;
    private ArmorData[] allArmors;
    private ArtifactData[] allArtifacts;

    [SerializeField] private GameObject firstChestPopUp;
    [SerializeField] private GameObject chestPopUp;
    [SerializeField] private Transform firstChestChoices;
    [SerializeField] private Transform chestChoices;
    private int i = 0, j = 0, k = 0;

    [SerializeField] private GameObject movingWeapon;

    public void Awake()
    {
        allWeapons = Resources.LoadAll<WeaponData>("Weapons/");
        allArmors = Resources.LoadAll<ArmorData>("Armors/");
        allArtifacts = Resources.LoadAll<ArtifactData>("Artifacts/");
    }

    public void OpenFirstChest()
    {
        firstChestPopUp.SetActive(true);
        i = Random.Range(0, allWeapons.Length);
        firstChestChoices.GetChild(1).GetChild(0).GetComponent<Image>().sprite = allWeapons[i].visual;
        Time.timeScale = 0f;
    }

    public void OpenChest()
    {
        chestPopUp.SetActive(true);
        i = Random.Range(0, allWeapons.Length);
        chestChoices.GetChild(0).GetChild(0).GetComponent<Image>().sprite = allWeapons[i].visual;
        j = Random.Range(0, allArmors.Length);
        chestChoices.GetChild(1).GetChild(0).GetComponent<Image>().sprite = allArmors[j].visual;
        k = Random.Range(0, allArtifacts.Length);
        chestChoices.GetChild(2).GetChild(0).GetComponent<Image>().sprite = allArtifacts[k].visual;
        Time.timeScale = 0f;
    }

    public void ClickWeapon()
    {
        weapon = allWeapons[i];
        invPanel.GetChild(0).GetChild(0).GetComponent<Image>().sprite = allWeapons[i].visual;
        movingWeapon.SetActive(true);
        movingWeapon.GetComponent<SpriteRenderer>().sprite = allWeapons[i].visual;
        GameObject player = GameObject.Find("Player");
        Destroy(player.transform.GetChild(0).gameObject);
        GameObject weaponObj = Instantiate(weapon.weaponPrefab, player.transform);
        weaponObj.GetComponent<SpriteRenderer>().sprite = weapon.visual;
        weaponObj.SetActive(true);
        Time.timeScale = 1f;
        firstChestPopUp.SetActive(false);
        chestPopUp.SetActive(false);
    }

    public void ClickArmor()
    {
        armor = allArmors[j];
        invPanel.GetChild(1).GetChild(0).GetComponent<Image>().sprite = allArmors[j].visual;
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }

    public void ClickArtifact()
    {
        artifact = allArtifacts[k];
        invPanel.GetChild(2).GetChild(0).GetComponent<Image>().sprite = allArtifacts[k].visual;
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }
}
