using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private float idleTime = 0f;
    private float idleThreshold = 1.5f;
    private bool cursorVisible = true;

    void Start()
    {
        Cursor.visible = false;
        cursorVisible = false;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (!cursorVisible)
            {
                Cursor.visible = true;
                cursorVisible = true;
            }
            idleTime = 0f; 
        }
        else
        {
            idleTime += Time.deltaTime;
        }

        if (cursorVisible && idleTime > idleThreshold)
        {
            Cursor.visible = false;
            cursorVisible = false;
        }
    }
}
