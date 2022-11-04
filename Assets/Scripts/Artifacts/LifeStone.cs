using UnityEngine;

/// <summary>
/// Create an artifact increasing the maximum life of the player
/// </summary>
public class LifeStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private HealthBar hb;
    private PlayerHealth ph;

    /// <summary>
    /// Reset the value in the scriptable object
    /// </summary>
    ~LifeStone()
    {
        stoneData.maxHealth = 50;
    }

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
        /* if the current life is superior to the max health without the artifact */
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
        stoneData.level += 1;
        ph.maxHealth -= stoneData.maxHealth;
        stoneData.maxHealth = Mathf.RoundToInt(stoneData.maxHealth * 1.2f); /* Increase the value in the scriptable object */
        ph.maxHealth += stoneData.maxHealth;
        hb.SetMaxHealth(ph.maxHealth); /* Apply the new value */
    }
}
