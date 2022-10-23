using UnityEngine;

public class Clover : Artifact
{
    [SerializeField]
    private ArtifactData cloverData;
    private GameObject[] enemies;
    private bool effectApplied = false;

    private void Update()
    {
        if (Time.timeSinceLevelLoad == 0 && effectApplied)
            effectApplied = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!effectApplied)
        {
            effectApplied = true;
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<Enemy>().dropRate *= cloverData.value;
        }
    }
}
