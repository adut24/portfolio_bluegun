using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fire_column : MonoBehaviour
{
    Tilemap test;
    // Start is called before the first frame update
    void Start()
    {
        Bounds bound = gameObject.GetComponent<Tilemap>().GetBoundsLocal();
        test = gameObject.GetComponent<Tilemap>();
        test.GetTilesBlock()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
