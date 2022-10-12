using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private Animator animator;
    private new CapsuleCollider2D collider;
    private Animator fadeSystem;
    public string sceneName;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        fadeSystem = GameObject.Find("FadeSystem").GetComponent<Animator>();
        animator.enabled = false;
        collider.enabled = false;
    }

    private void Update()
    {
        GC.Collect();   /* Start the Garbage Collector */
        if (Enemy.enemyNumber == 0)
        {
            StartCoroutine(EnablePortal());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator EnablePortal()
    {
        if (GameObject.Find("Lock"))
        {
            GameObject.Find("Lock").GetComponent<AudioSource>().enabled = true;
            yield return new WaitForSeconds(1f);
            Destroy(GameObject.Find("Lock"));
        }
        animator.enabled = true;
        collider.enabled = true;

    }

    private IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
