using UnityEngine;

public class SpeedStone : Artifact
{
    [SerializeField]
    private ArtifactData stoneData;
    private PlayerMovement pm;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public override void Add()
    {
        pm.moveSpeed *= stoneData.speed;
    }

    public override void Remove()
    {
        pm.moveSpeed /= stoneData.speed;
    }
}
