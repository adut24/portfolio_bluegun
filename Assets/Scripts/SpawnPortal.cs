using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private Animator portalAnimation;
    private Animator fadeSystem;
    private GameObject chest;
    private GameObject spawnedChest;
    private GameObject[] enemies;
    public string sceneName;

    private void Start()
    {
        portalAnimation = gameObject.GetComponent<Animator>();
        fadeSystem = GameObject.Find("FadeSystem").GetComponent<Animator>();
        portalAnimation.enabled = false;
        chest = Resources.Load<GameObject>("Chest");
        if (SceneManager.GetActiveScene().name != "Introduction")
        {
            spawnedChest = Instantiate(chest, RoomGenerator.VerifySpawn(RoomGenerator.floorPositions), Quaternion.identity);
            spawnedChest.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            if (spawnedChest && !spawnedChest.GetComponent<BoxCollider2D>().isTrigger)
                spawnedChest.GetComponent<BoxCollider2D>().isTrigger = true;
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
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(1f);
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        portalAnimation.enabled = true;
    }

    private IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
