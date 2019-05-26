using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// allows the GameObject to be grabbed by mouse and touch
/// last edited: Evan Cheng
/// </summary>
public class Grabable : MonoBehaviour
{
    public bool grabbable; 

    void OnMouseDrag()
    {
        if (grabbable)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        }
    }
}
