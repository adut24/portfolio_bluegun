using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Animations;

public class FireTrap : MonoBehaviour
{
    public GameObject firePrefab;
    public Tilemap floor;
    public BoundsInt mapBounds;
    private List<Animator> animations = new List<Animator>();
    // Start is called before the first frame update
    public IEnumerator FireLine(float j)
    {
        Debug.Log("Min:" + mapBounds.yMin + " | Max:" + mapBounds.yMax);

            for (float i = mapBounds.xMin + 0.5f; i < mapBounds.xMax; i += 1.0f)
            {
                GameObject trap = Instantiate(firePrefab);
                animations.Add(trap.GetComponent<Animator>());
                trap.transform.Translate(i, j, 0);
                yield return new WaitForSeconds(0.02f);
            }
    }

    public IEnumerator FireEvent()
    {
        for (float j = mapBounds.yMax - 10/*mapBounds.yMin + 0.5f*/; j < mapBounds.yMax; j += 1.0f)
        {
            StartCoroutine(FireLine(j));
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2.0f);
        foreach (Animator anim in animations)
        {
            anim.SetBool("FireStop", true);
            anim.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        animations = new List<Animator>();
    }

    void Start()
    {
        floor.CompressBounds();
        Debug.Log(floor.size);
        mapBounds = GameObject.Find("Floor").GetComponent<Tilemap>().cellBounds;
        StartCoroutine(FireEvent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
