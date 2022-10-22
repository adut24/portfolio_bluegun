using UnityEngine;

public class Clover : Artifact
{
    [SerializeField]
    private ArtifactData cloverData;
    private GameObject[] enemies = {};

    private void Update()
    {
        if (Time.timeSinceLevelLoad == 0 && enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().dropRate *= cloverData.value;
            }
        }
    }
}
