using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{

    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        this.cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        this.player = GameObject.FindGameObjectWithTag("Boss");
    }

    protected override void Update()
    {
    }

    protected override void launchProjectile(Vector3 startPosition, Vector3 direction)
    {
        for (int i = 0; i < projectileNumber; i++)
        {
            Projectile proj = Instantiate(projectilePrefab, startPosition, transform.rotation);
            proj.speed = Random.Range(projectileSpeed - projectileSpeedRange, projectileSpeed + projectileSpeedRange);
            proj.parent = this;
            proj.direction = direction;
            proj.GetComponent<Transform>().localScale = new Vector3(size, size, 1);
            proj.pierce = pierce;

            if (spread > 0)
            {
                float variance = (i * spread) / projectileNumber + Time.fixedTime * 10;
                variance *= Mathf.Deg2Rad;
                float varAngle = 2.5f * Mathf.PI - Mathf.Atan2 (direction.y, direction.x) + variance;
                proj.direction = new Vector3(Mathf.Sin(varAngle), Mathf.Cos(varAngle), 1);
                proj.transform.position += proj.direction * projectileOffset;
            }
        }

    }

    public override void ShootWeapon()
    {
        if (shootPause == false)
        {
            v3Pos = cam.WorldToScreenPoint(player.transform.position);
            v3Pos = GameObject.FindGameObjectWithTag("Player").transform.position - v3Pos;

            Vector3 startPosition;
            Vector3 direction;

            direction = Vector3.Normalize(v3Pos);
            startPosition = player.transform.position;
            launchProjectile(startPosition, direction);
            shootCoroutine = StartCoroutine(ResumeShoot());
            AudioSource source = GetComponent<AudioSource>();
        }
    }

}
