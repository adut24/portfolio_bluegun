using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float    speed = 4.5f;
    public Vector3  direction;
    public int      damage = 5;
    public float    duration = 1.0f;
    public int      pierce = 0;

    void Start()
    {
        Destroy(gameObject, duration);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // transform.position += direction * speed * Time.deltaTime; Linear move
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed); // Incremental move
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
            if (pierce > 0)
                pierce--;
            else
                Destroy(gameObject);
        }
    }


}
