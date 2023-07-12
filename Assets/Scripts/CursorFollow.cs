using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CursorFollow : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        transform.position = UnityEngine.Input.mousePosition;
    }
}
