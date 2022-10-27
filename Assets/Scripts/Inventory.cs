using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private GameObject heldArtifact;
    [SerializeField] private GameObject heldArmor;

    private GameObject player;

    public void Awake()
    {
        allWeapons = Resources.LoadAll<WeaponData>("Weapons/");
        allArmors = Resources.LoadAll<ArmorData>("Armors/");
        allArtifacts = Resources.LoadAll<ArtifactData>("Artifacts/");
        player = GameObject.Find("Player");
    }

    public void OpenFirstChest()
    {
        firstChestPopUp.SetActive(true);
        Slot slot = firstChestChoices.GetChild(0).GetComponent<Slot>();
        slot.weapon = allWeapons[0];
        slot.visual.sprite = allWeapons[0].visual;

        slot = firstChestChoices.GetChild(1).GetComponent<Slot>();
        slot.weapon = allWeapons[1];
        slot.visual.sprite = allWeapons[1].visual;

        slot = firstChestChoices.GetChild(2).GetComponent<Slot>();
        slot.weapon = allWeapons[2];
        slot.visual.sprite = allWeapons[2].visual;
        Time.timeScale = 0f;
    }

    public void OpenChest()
    {
        chestPopUp.SetActive(true);
        i = Random.Range(0, allWeapons.Length);
        Slot weaponSlot = chestChoices.GetChild(0).GetComponent<Slot>();
        weaponSlot.weapon = allWeapons[i];
        weaponSlot.visual.sprite = allWeapons[i].visual;

        j = Random.Range(0, allArmors.Length);
        Slot armorSlot = chestChoices.GetChild(1).GetComponent<Slot>();
        armorSlot.armor = allArmors[j];
        armorSlot.visual.sprite = allArmors[j].visual;

        k = Random.Range(0, allArtifacts.Length);
        k = 3;
        Slot artifactSlot = chestChoices.GetChild(2).GetComponent<Slot>();
        artifactSlot.artifact = allArtifacts[k];
        artifactSlot.visual.sprite = allArtifacts[k].visual;
        Time.timeScale = 0f;
    }

    public void ClickFirstWeapon()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "First")
            i = 0;
        else if (EventSystem.current.currentSelectedGameObject.name == "Second")
            i = 1;
        else
            i = 2;
        ClickWeapon();
    }

    public void ClickWeapon()
    {
        if (weapon != allWeapons[i])
        {
            weapon = allWeapons[i];
            Slot weaponSlot = invPanel.GetChild(0).GetComponent<Slot>();
            weaponSlot.weapon = allWeapons[i];
            weaponSlot.visual.sprite = allWeapons[i].visual;
            movingWeapon.SetActive(true);
            movingWeapon.GetComponent<SpriteRenderer>().sprite = allWeapons[i].visual;
            Destroy(GameObject.FindWithTag("Weapon"));
            movingWeapon = Instantiate(weapon.weaponPrefab, player.transform);
            movingWeapon.GetComponent<SpriteRenderer>().sprite = weapon.visual;
            movingWeapon.SetActive(true);
            firstChestPopUp.SetActive(false);
            chestPopUp.SetActive(false);
            TooltipSystem.instance.Hide();
            Time.timeScale = 1f;
        }
    }

    public void ClickArmor()
    {
        if (armor != allArmors[j])
        {
            armor = allArmors[j];
            Slot armorSlot = invPanel.GetChild(1).GetComponent<Slot>();
            armorSlot.armor = allArmors[j];
            armorSlot.visual.sprite = allArmors[j].visual;
            heldArmor.GetComponent<Armor>().Remove();
            Destroy(heldArmor);
            heldArmor = Instantiate(armor.armorPrefab, player.transform);
            heldArmor.GetComponent<Armor>().armor = armor;
            heldArmor.SetActive(true);
            heldArmor.GetComponent<Armor>().Add();
            chestPopUp.SetActive(false);
            TooltipSystem.instance.Hide();
            Time.timeScale = 1f;
        }
    }

    public void ClickArtifact()
    {
        if (artifact != allArtifacts[k])
        {
            artifact = allArtifacts[k];
            Slot artifactSlot = invPanel.GetChild(2).GetComponent<Slot>();
            artifactSlot.artifact = allArtifacts[k];
            artifactSlot.visual.sprite = allArtifacts[k].visual;
            heldArtifact.SetActive(true);
            heldArtifact.GetComponent<Artifact>().Remove();
            Destroy(GameObject.FindWithTag("Artifact"));
            heldArtifact = Instantiate(artifact.artifactPrefab, player.transform);
            heldArtifact.GetComponent<Artifact>().Add();
        }
        else
            heldArtifact.GetComponent<Artifact>().Upgrade();
        TooltipSystem.instance.Hide();
        chestPopUp.SetActive(false);
        Time.timeScale = 1f;

    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }
}
