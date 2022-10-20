using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Weapon parent;
    public Vector3  direction;
    public int      pierce = 0;
    public int slowCount = 100;
    public float speed = 5.0f;
    void Start()
    {
        Destroy(gameObject, parent.projectileDuration);
        if (parent.projectileMovement == Weapon.moveType.Decremental)
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed);// Incremental move
        slowCount = 100;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch (parent.projectileMovement)
        {
            case Weapon.moveType.Linear:
                transform.position += direction * speed * Time.deltaTime;// Linear move
                break;

            case Weapon.moveType.Incremental:
                gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed);// Incremental move
                break;

            case Weapon.moveType.Decremental:
                if (slowCount > 0)
                {
                    slowCount--;
                    gameObject.GetComponent<Rigidbody2D>().AddForce((direction * speed) * -0.01f);// Incremental move
                }
                break;
        }
        //gameObject.GetComponent<Rigidbody2D>().AddForce(direction * parent.projectileSpeed);// Incremental move
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Walls")
        {
            Destroy(gameObject);
            return ;
        }
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && enemy.alive == true)
        {
            if (parent.dot.enabled == true)
            {
                Debuff dot = Instantiate(parent.dot.debuffPrefab, enemy.transform);
                dot.dot = parent.dot;
                dot.enemy = enemy;
            }
            if (pierce > 0)
                pierce--;
            else
                Destroy(gameObject);
        }
    }
}
