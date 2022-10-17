using System.Runtime.CompilerServices;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Sprite cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor.texture, UnityEngine.Vector2.zero, CursorMode.Auto);
    }


}
