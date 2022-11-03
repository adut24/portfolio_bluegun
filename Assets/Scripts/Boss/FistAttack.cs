using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistAttack : Attack
{
    private static bool flip = false;
    private bool dmgDone = false;
    private GameObject player;
    private GameObject boss;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
        transform.position = player.transform.position;
        StartCoroutine(Attack(currentPhase));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RotatePreview(float followspeed, float duration, float attackDelay)
    {
        int i = 72;
        while (i > 0)
        {
            transform.Rotate(0.0f, 0.0f, 5);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followspeed);
            yield return new WaitForSeconds((duration * Time.deltaTime) / 72);
            i--;
        }
        transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(attackDelay);
    }

    public IEnumerator MoveFist(float fistSpeed)
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
            yield return new WaitForSeconds(fistSpeed);
        }
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake(0.5f, 1));
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


    public IEnumerator Attack(int phase)
    {
        switch (phase)
        {
            case 0:
                yield return StartCoroutine(RotatePreview(0.5f, 2.0f, 0.001f));
                yield return StartCoroutine(MoveFist(0.05f));
                break;
            case 1:
                yield return StartCoroutine(RotatePreview(0.5f, 2.0f, 0.5f));
                yield return StartCoroutine(MoveFist(0.02f));
                break;
            case 2:
                yield return StartCoroutine(RotatePreview(0.5f, 2.0f, 0.5f));
                yield return StartCoroutine(MoveFist(0));
                break;
            default:
                break;
        }
        flip = !flip;
        Destroy(gameObject, 0.5f);
    }
}
