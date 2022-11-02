using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : MonoBehaviour
{
    private static bool flip = false;
    private bool dmgDone = false;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RotatePreview()
    {
        int i = 72;
        while (i > 0)
        {
            transform.Rotate(0.0f, 0.0f, 5);
            yield return new WaitForSeconds(2.0f / 72);
            i--;
        }
        transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator MoveFist()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Transform fistPos = transform.GetChild(0).transform;
        if (flip == true)
        {
            fistPos.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //fistPos.rotation = Quaternion.identity;
        //fistPos.localRotation = Quaternion.identity;
        while (fistPos.localPosition.y > 0.05f)
        {
            fistPos.localPosition = new Vector3(fistPos.localPosition.x, fistPos.localPosition.y - 0.05f);
            yield return new WaitForSeconds(2.0f / 72);
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake(0.5f, 1));
        Destroy(gameObject, 0.5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (dmgDone == false)
        {
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(30);
                dmgDone = true;
            }
        }
    }


    public IEnumerator Attack()
    {
        Debug.Log("This goes here");
        yield return StartCoroutine(RotatePreview());
        StartCoroutine(MoveFist());
        flip = !flip;
    }
}
