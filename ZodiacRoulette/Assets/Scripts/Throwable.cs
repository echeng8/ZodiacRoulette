using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows players to throw the object (enabling gravity) if moving fast enough. 
/// Otherwise, return the object to their position at Start.
/// last edited: Evan
/// </summary>
public class Throwable : Grabable
{
    //note: pokeball in pogo has constant speed and follows the mouse at it; our version follows the mouse 1:1 - ec
    public float maxThrowVelocity, minVToThrow;
    Vector3 startingPosition;  
    float throwVelocity;

    private void Start()
    {
        startingPosition = transform.position; 
    }
    private void Update()
    {
        throwVelocity = Mathf.Clamp01((new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))).magnitude) *  maxThrowVelocity;
        Debug.Log(throwVelocity); 
    }

    private void OnMouseUp()
    {
        if(grabbable)
        {
            if (throwVelocity > minVToThrow)
            {
                Vector3 throwDirection = (Vector3.forward + Vector3.)
                GetComponent<Rigidbody>().AddForce(Vector3.forward * throwVelocity);
                GetComponent<Rigidbody>().useGravity = true; 
                grabbable = false;
            }
            else
            {
                transform.position = startingPosition; 
            }

        }
    }
}
