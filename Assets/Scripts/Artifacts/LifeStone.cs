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
        if (ph.currenthealth > ph.maxHealth - stoneData.maxHealth)
        {
            ph.maxHealth -= stoneData.maxHealth;
            hb.SetMaxHealth(ph.maxHealth);
            ph.currenthealth = ph.maxHealth;
            hb.SetHealth(ph.currenthealth);
        }
        else
        {
            ph.maxHealth -= stoneData.maxHealth;
            hb.SetMaxHealth(ph.maxHealth);
        }
    }

    public override void Upgrade()
    {
        ph.maxHealth -= stoneData.maxHealth;
        stoneData.maxHealth = (int)(stoneData.maxHealth * 1.2f);
        ph.maxHealth += stoneData.maxHealth;
        hb.SetMaxHealth(ph.maxHealth);
    }
}
