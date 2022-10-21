using UnityEngine;

public class Clover : Artifact
{
    private GameObject[] enemies = {};

    private void Update()
    {
        if (Time.timeSinceLevelLoad == 0 && enemies.Length == 0)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().dropRate *= 4;
            }
        }
    }

    public override void Remove()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().dropRate /= 4;
        }
    }
}
