using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;
    private new CapsuleCollider2D collider;
    private Animator fadeSystem;
    public string sceneName;

    private void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        fadeSystem = GameObject.Find("FadeSystem").GetComponent<Animator>();
        sprite.enabled = false;
        animator.enabled = false;
        collider.enabled = false;
    }

    private void Update()
    {
        GC.Collect();   /* Start the Garbage Collector */
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
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
