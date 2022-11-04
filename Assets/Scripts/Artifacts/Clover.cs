using UnityEngine;

/// <summary>
/// Create an artifact increasing the potion drop rate on the enemies
/// </summary>
public class Clover : Artifact
{
    [SerializeField]
    private ArtifactData cloverData;
    private GameObject[] enemies;
    private bool effectApplied = false;

    /// <summary>
    /// Reset the value in the scriptable object
    /// </summary>
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
                enemy.GetComponent<Enemy>().dropRate = Mathf.RoundToInt(enemy.GetComponent<Enemy>().dropRate * cloverData.value);
            effectApplied = true;
        }
    }

    public override void Upgrade()
    {
        cloverData.level += 1;
        cloverData.value *= 1.15f;
    }
}
