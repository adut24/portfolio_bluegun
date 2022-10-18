using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float    speed = 4.5f;
    public Vector3  direction;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed);
        Debug.Log(gameObject.GetComponent<Rigidbody2D>().velocity);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("me am here");
        Destroy(gameObject);
    }
}
