using UnityEngine;

public class SpeedStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private PlayerMovement pm;
    private PlayerHealth ph;
    private bool effectApplied = false;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        int percentHealth = ph.currenthealth * 100 / ph.maxHealth;

        if (!effectApplied && percentHealth <= 30)
        {
            effectApplied = true;
            pm.moveSpeed *= stoneData.speed;
        }
        else if (effectApplied && percentHealth > 30)
        {
            effectApplied = false;
            pm.moveSpeed /= stoneData.speed;
        }
    }

    public override void Remove()
    {
        if (effectApplied)
            pm.moveSpeed /= stoneData.speed;
    }

    /*
    public override void Add()
    {
        pm.moveSpeed *= stoneData.speed;
    }

    public override void Remove()
    {
        pm.moveSpeed /= stoneData.speed;
    }
    */
}
