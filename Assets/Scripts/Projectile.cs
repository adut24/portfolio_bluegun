using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Weapon parent;
    public Vector3  direction;
    public int      pierce = 0;
    void Start()
    {
        Destroy(gameObject, parent.projectileDuration);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position += direction * speed * Time.deltaTime; Linear move
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * parent.projectileSpeed); // Incremental move
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
                Debuff dot = Instantiate(parent.dot.debuffPrefab);
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
