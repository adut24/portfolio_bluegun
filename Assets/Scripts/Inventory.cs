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
        slot.weaponVisual.sprite = allWeapons[0].visual;

        slot = firstChestChoices.GetChild(1).GetComponent<Slot>();
        slot.weapon = allWeapons[1];
        slot.weaponVisual.sprite = allWeapons[1].visual;

        slot = firstChestChoices.GetChild(2).GetComponent<Slot>();
        slot.weapon = allWeapons[2];
        slot.weaponVisual.sprite = allWeapons[2].visual;
        Time.timeScale = 0f;
    }

    public void OpenChest()
    {
        chestPopUp.SetActive(true);
        i = Random.Range(0, allWeapons.Length);
        Slot slot = chestChoices.GetChild(0).GetComponent<Slot>();
        slot.weapon = allWeapons[i];
        slot.weaponVisual.sprite = allWeapons[i].visual;

        j = Random.Range(0, allArmors.Length);
        chestChoices.GetChild(1).GetChild(0).GetComponent<Image>().sprite = allArmors[j].visual;

        k = Random.Range(0, allArtifacts.Length);
        chestChoices.GetChild(2).GetChild(0).GetComponent<Image>().sprite = allArtifacts[k].visual;
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
        weapon = allWeapons[i];
        Slot weaponSlot = invPanel.GetChild(0).GetComponent<Slot>();
        weaponSlot.weapon = allWeapons[i];
        weaponSlot.weaponVisual.sprite = allWeapons[i].visual;
        movingWeapon.SetActive(true);
        movingWeapon.GetComponent<SpriteRenderer>().sprite = allWeapons[i].visual;
        Destroy(player.transform.GetChild(0).gameObject);
        movingWeapon = Instantiate(weapon.weaponPrefab, player.transform);
        movingWeapon.GetComponent<SpriteRenderer>().sprite = weapon.visual;
        movingWeapon.SetActive(true);
        firstChestPopUp.SetActive(false);
        chestPopUp.SetActive(false);
        TooltipSystem.instance.Hide();
        Time.timeScale = 1f;
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
        heldArtifact.SetActive(true);
        heldArtifact.GetComponent<Artifact>().Remove();
        heldArtifact = Instantiate(artifact.artifactPrefab, player.transform);
        heldArtifact.GetComponent<Artifact>().Add();
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }

    public void ClosePopUp()
    {
        Time.timeScale = 1f;
        chestPopUp.SetActive(false);
    }
}
