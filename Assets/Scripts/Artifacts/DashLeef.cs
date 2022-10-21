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

    public override void Remove()
    {
        pm.dashingCooldown = 2f;
        pm.fillValue /= 2;
    }
}
