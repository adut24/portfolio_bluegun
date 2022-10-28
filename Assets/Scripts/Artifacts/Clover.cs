using UnityEngine;

public class Clover : Artifact
{
    [SerializeField]
    private ArtifactData cloverData;
    private GameObject[] enemies;
    private bool effectApplied = false;

    ~Clover()
    {
        cloverData.value = 4f;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad == 0 && enemies.Length == 0)
            effectApplied = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (!effectApplied && enemies.Length != 0)
        {
            foreach (GameObject enemy in enemies)
                enemy.GetComponent<Enemy>().dropRate = (int)(enemy.GetComponent<Enemy>().dropRate * cloverData.value);
            effectApplied = true;
        }
    }

    public override void Upgrade()
    {
        cloverData.value *= 1.15f;
    }
}
