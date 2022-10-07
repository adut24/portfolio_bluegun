using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private GameObject portal;
    private SpriteRenderer sprite;
    private Animator animator;
    private new CapsuleCollider2D collider;
    public string sceneName;

    private void Start()
    {
        portal = GameObject.Find("Portal");
        sprite = portal.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        animator = portal.GetComponent<Animator>();
        animator.enabled = false;
        collider = portal.GetComponent<CapsuleCollider2D>();
        collider.enabled = false;
    }

    private void Update()
    {
        GC.Collect();
        if (Enemy.enemyNumber == 0)
        {
            sprite.enabled = true;
            animator.enabled = true;
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);
    }
}
