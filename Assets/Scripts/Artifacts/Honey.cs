using UnityEngine;
using System.Collections;

public class Honey : Artifact
{
    [SerializeField]
    private ArtifactData honeyData;
    private bool isInInv = false;
    private bool canHeal = true;

    private PlayerHealth ph;

    public void Awake()
    {
        ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    public override void Add()
    {
        isInInv = true;
    }

    public void Update()
    {
        if (isInInv && Input.GetKeyDown(KeyCode.R))
        {
            if (canHeal)
            {
                canHeal = false;
                ph.RegenerateLife(honeyData.health);
                StartCoroutine(HoneyCooldown());
            }
        }
    }

    public IEnumerator HoneyCooldown()
    {
        yield return new WaitForSeconds(honeyData.artifactActivation);
        canHeal = true;
    }

    public override void Remove()
    {
        isInInv = false;
    }
}
