using UnityEngine;

public class DashLeef : Artifact
{
    [SerializeField]
    private ArtifactData dashLeefData;
    private PlayerMovement pm;

    public void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public override void Add()
    {
        pm.dashingCooldown = dashLeefData.dashCooldown;
        pm.fillValue *= 2;
    }

    public override void Upgrade()
    {
        dashLeefData.level += 1;
        pm.dashingCooldown *= 0.8f;
        pm.fillValue /= 0.8f;
    }

    public override void Remove()
    {
        pm.dashingCooldown = 2f;
        pm.fillValue = 0.45f;
    }
}
