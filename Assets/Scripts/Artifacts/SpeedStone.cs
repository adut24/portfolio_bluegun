using UnityEngine;

/// <summary>
/// Create an artifact increasing the speed of the player
/// </summary>
public class SpeedStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private PlayerMovement pm;
    private PlayerHealth ph;
    private bool effectApplied = false;
    private int percentHealth;

    /// <summary>
    /// Reset the value in the scriptable object
    /// </summary>
    ~SpeedStone()
    {
        stoneData.speed = 0.5f;
    }

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        percentHealth = ph.currenthealth * 100 / ph.maxHealth;

        if (!effectApplied && percentHealth <= 30) /* if the player has 30 % or less life remaining and the effect isn't applied */
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
        stoneData.level += 1;
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
