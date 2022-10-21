using UnityEngine;

public class LifeStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private HealthBar hb;
    private PlayerHealth ph;

    private void Awake()
    {
        hb = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void Add()
    {
        ph.maxHealth += stoneData.maxHealth;
        hb.SetMaxHealth(ph.maxHealth);
    }

    public override void Remove()
    {
        ph.maxHealth -= stoneData.maxHealth;
        hb.SetMaxHealth(ph.maxHealth);
    }
}
