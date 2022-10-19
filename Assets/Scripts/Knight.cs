using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    private List<GameObject> children;
    private bool isAttacking = false;


    protected override void Start()
    {
        base.Start();
        children = new();
        children.Add(transform.Find("Slash").gameObject);
        children.Add(transform.Find("End").gameObject);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player && Vector2.Distance(transform.position, player.transform.position) <= minDistance && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        base.Flip(transform.position, player.transform.position);
        if (sprite.flipX)
        {
            foreach (GameObject child in children)
            {
                child.transform.Rotate(new Vector2(0, 180));
            }
        }
        anim.Play("KnightAttack");
        yield return new WaitForSeconds(2.5f);
        isAttacking = false;
    }
}
