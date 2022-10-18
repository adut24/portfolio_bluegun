using UnityEngine;

public class Sorcerer : Enemy
{
    private Animator attackAnim;
    private GameObject firedProjectile, projectile;

    protected override void Start()
    {
        base.Start();
        attackAnim = GetComponent<Animator>();
        projectile = Resources.Load<GameObject>("EnemyProjectile");
    }

    protected override void Update()
    {
        base.Update();

        if (player && Vector2.Distance(transform.position, player.transform.position) <= minDistance && !firedProjectile)
            attackAnim.enabled = true;
        else
            attackAnim.enabled = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void Attack()
    {
        Vector2 firePosition = DetermineSide();
        firedProjectile = Instantiate(projectile, firePosition, transform.rotation);
        if (firedProjectile)
            Destroy(firedProjectile, 1.5f);
    }

    private Vector2 DetermineSide()
    {
        if (transform.position.x > player.transform.position.x)
            return transform.Find("Left").position;
        else
            return transform.Find("Right").position;
    }
}
