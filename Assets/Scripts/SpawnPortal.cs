using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage the portal and the reward chest
/// </summary>
public class SpawnPortal : MonoBehaviour
{
    private Animator portalAnimation;
    private Animator fadeSystem;
    private AudioSource sound;
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
            chestSprite.color = new Color(1f, 1f, 1f, 0.5f); /* Make the chest half transparent to show it's not active */
        }
        sound = transform.GetChild(0).GetComponent<AudioSource>();
        sound.enabled = false;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); /* Look at the enemies remaining */
        if (enemies.Length == 0)  /* if there is no enemy remaining */
        {
            EnablePortal();
            if (spawnedChest)
            {
                chestCollider.enabled = true;                  /* "Activate" the chest */
                chestSprite.color = new Color(1f, 1f, 1f, 1f); /* Make the chest fully visible */
            }
            portalCollider.enabled = true;                     /* "Activate" the portal */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private void EnablePortal()
    {
        if (transform.childCount > 0) /* If the lock is still present */
        {
            sound.enabled = true;

            if (!sound.isPlaying)
                Destroy(transform.GetChild(0).gameObject); /* Destroy the lock */
        }
        portalAnimation.enabled = true;
    }

    private IEnumerator LoadNextScene()
    {
        fadeSystem.SetTrigger("FadeIn");        /* Start the FadeIn animation */
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);      /* Load the next scene */
    }
}
