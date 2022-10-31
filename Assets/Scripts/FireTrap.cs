using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireTrap : MonoBehaviour
{
    public GameObject firePrefab;
    public Tilemap floor;
    public BoundsInt mapBounds;
    // Start is called before the first frame update
    public IEnumerator FireLine(int y)
    {
        Debug.Log("Min:" + mapBounds.yMin + " | Max:" + mapBounds.yMax);
        for (float j = mapBounds.yMin + 0.5f; j < mapBounds.yMax; j += 1.0f)
        {
            for (float i = mapBounds.xMin + 0.5f; i < mapBounds.xMax; i += 1.0f)
            {
                GameObject trap = Instantiate(firePrefab);
                trap.transform.Translate(i, j, 0);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }

    void Start()
    {
        floor.CompressBounds();
        Debug.Log(floor.size);
        mapBounds = GameObject.Find("Floor").GetComponent<Tilemap>().cellBounds;
        StartCoroutine(FireLine(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
