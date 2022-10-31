using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPortal : MonoBehaviour
{
    private Animator portalAnimation;
    private Animator fadeSystem;
    private GameObject chest;
    private GameObject spawnedChest;
    private CapsuleCollider2D portalCollider;
    private BoxCollider2D chestCollider;
    private SpriteRenderer chestSprite;
    private GameObject[] enemies;
    public string sceneName;

    private void Start()
    {
        portalAnimation = GetComponent<Animator>();
        fadeSystem = GameObject.Find("FadeSystem").GetComponent<Animator>();
        portalCollider = GetComponent<CapsuleCollider2D>();
        portalAnimation.enabled = false;
        portalCollider.enabled = false;
        if (SceneManager.GetActiveScene().name != "Introduction")
        {
            chest = Resources.Load<GameObject>("Chest");
            spawnedChest = Instantiate(chest, RoomGenerator.VerifySpawn(RoomGenerator.floorPositions), Quaternion.identity);
            chestCollider = spawnedChest.GetComponent<BoxCollider2D>();
            chestSprite = spawnedChest.GetComponent<SpriteRenderer>();
            chestCollider.enabled = false;
            chestSprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            if (spawnedChest)
            {
                chestCollider.enabled = true;
                chestSprite.color = new Color(1f, 1f, 1f, 1f);
            }
            portalCollider.enabled = true;
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
