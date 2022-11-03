using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.Animations;

public class FireTrap : Attack
{
    public GameObject firePrefab;
    public Tilemap floor;
    public BoundsInt mapBounds;
    public static Dictionary<int, bool> reservedRows = new Dictionary<int, bool>();
    public IEnumerator FireLine(float j, float direction)
    {
        reservedRows[(int) j] = true;
        List<Animator> lineAnims = new List<Animator>();
        float start, end;
        if (direction == 1.0f)
        {
            start = mapBounds.xMin + 0.5f;
            end = mapBounds.xMax;
            for (float i = start; i < end; i += direction)
            {
                GameObject trap = Instantiate(firePrefab, transform);
                lineAnims.Add(trap.GetComponent<Animator>());
                trap.transform.Translate(i, j, 0);
                trap.GetComponent<Animator>().SetBool("PreviewStart", true);
                yield return null;
            }
        }
        else
        {
            end = mapBounds.xMin;
            start = mapBounds.xMax - 0.5f;
            for (float i = start; i > end; i += direction)
            {
                GameObject trap = Instantiate(firePrefab);
                lineAnims.Add(trap.GetComponent<Animator>());
                trap.transform.Translate(i, j, 0);
                trap.GetComponent<SpriteRenderer>().enabled = true;
                yield return null;
            }
        }
        // animations.AddRange(lineAnims);
        yield return new WaitForSeconds(2);
        foreach (Animator anim in lineAnims)
        {
            anim.Rebind();
            anim.Update(0f);
            anim.SetBool("AnimStart", true);
        }
        yield return new WaitForSeconds(3.0f);
        foreach (Animator anim in lineAnims)
        {
            anim.SetBool("FireStop", true);
        }
        yield return new WaitForSeconds(0.5f);
        reservedRows[(int) j] = false;
    }

    public IEnumerator FireEvent()
    {
        bool flip = true;
        int number = 1;
        int row = 0;
        switch (currentPhase)
        {
            case 0:
                number = 2;
                break;
            case 1:
                number = 4;
                break;
            case 2:
                number = 8;
                break;
        }
        Debug.Log(currentPhase);
        for (int i = 0; i < number; i++)
        {
            bool searchRow = true;
            while (searchRow == true)
            {
                row = Random.Range((int) mapBounds.yMin, (int) mapBounds.yMax);
                if (reservedRows.TryGetValue(row, out searchRow))
                {

                }
                else
                    searchRow = false;
            }
            StartCoroutine(FireLine((float) row, flip == true ? 1.0f : -1.0f));
            flip = !flip;
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.Find("Floor").GetComponent<Tilemap>();
        floor.CompressBounds();
        mapBounds = floor.cellBounds;
        StartCoroutine(FireEvent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
