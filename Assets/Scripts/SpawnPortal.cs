using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private Animator portalAnimation;
    private Animator fadeSystem;
    public string sceneName;
    private bool functionCalled = false;

    private void Start()
    {
        portalAnimation = gameObject.GetComponent<Animator>();
        fadeSystem = GameObject.Find("FadeSystem").GetComponent<Animator>();
        portalAnimation.enabled = false;
    }

    private void Update()
    {
        GC.Collect();   /* Start the Garbage Collector */
        if (Enemy.enemyNumber == 0 && !functionCalled)
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
        gameObject.transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        portalAnimation.enabled = true;
        functionCalled = true;
    }

    private IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
