using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows players to throw the object towards its forward transform (enabling gravity) if moving fast enough. 
/// Otherwise, return the object to their position at Start.
/// last edited: Evan
/// </summary>
public class Throwable : Grabable, IWillReset
{
    //note: pokeball in pogo has constant speed and follows the mouse at it; our version follows the mouse 1:1 - ec
    public float maxThrowVelocity, minVToThrow;
    public UnityEvent OnThrow;
    Vector3 startingPosition;
    float throwVelocity;

    private void Start()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        throwVelocity = Mathf.Clamp01((new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))).magnitude) * maxThrowVelocity;
    }

    private void OnMouseUp()
    {
        if (grabbable)
        {
            if (throwVelocity > minVToThrow)
            {

                Vector3 throwDirection = (transform.forward + (new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0))).normalized;
                GetComponent<Rigidbody>().AddForce(throwDirection * throwVelocity);
                GetComponent<Rigidbody>().useGravity = true;
                grabbable = false;
                OnThrow.Invoke();
            }
            else
            {
                transform.position = startingPosition;
            }

        }
    }

    public void resetGameObject()
    {
        grabbable = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = startingPosition; 
    }
}
