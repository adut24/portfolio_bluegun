using UnityEngine;

public class SpeedStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private PlayerMovement pm;
    private PlayerHealth ph;
    private bool effectApplied = false;
    private int percentHealth;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    ~SpeedStone()
    {
        stoneData.speed = 0.5f;
    }

    private void Update()
    {
        percentHealth = ph.currenthealth * 100 / ph.maxHealth;

        if (!effectApplied && percentHealth <= 30)
        {
            effectApplied = true;
            pm.speedModifier *= (1 + stoneData.speed);
        }
        else if (effectApplied && percentHealth > 30)
        {
            effectApplied = false;
            pm.speedModifier /= (1 + stoneData.speed);
        }
    }

    public override void Remove()
    {
        if (effectApplied)
            pm.speedModifier /= (1 + stoneData.speed);
    }

    public override void Upgrade()
    {
        if (effectApplied)
        {
            pm.speedModifier /= (1 + stoneData.speed);
            stoneData.speed *= 1.3f;
            pm.speedModifier *= (1 + stoneData.speed);
        }
        else
            stoneData.speed *= 1.3f;
    }
}
