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
    public float maxThrowForce, minVToThrow;
    public UnityEvent OnThrow;
   
    Vector3 startingPosition;
    float throwVelocity;

    [SerializeField] GameObject targeter; 
    [SerializeField] float timeTilRevealTargeter = 5f;
    [SerializeField] float _timer = 0;

    [SerializeField] Transform throwTarget; 

    GameManager game;

    [SerializeField] bool holding;

    private void Start()
    {
        game = GameManager.Instance;
        startingPosition = transform.position;
    }
    private void Update()
    {

        if(game.canThrow)
        {
            Vector2 inputVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            throwVelocity = Mathf.Clamp01((inputVector).magnitude) * maxThrowForce;
            if(!holding)
                _timer += Time.deltaTime;
            if (_timer > timeTilRevealTargeter)
            {
                if (!targeter.activeInHierarchy)
                    targeter.SetActive(true);
            }
        }
        else if (targeter.activeInHierarchy)
            targeter.SetActive(false);

    }

    private void OnMouseDown()
    {
        targeter.SetActive(false);
        _timer = 0;
        holding = true;
    }

    private void OnMouseUp()
    {
        if (grabbable && game.canThrow)
        {
            if (throwVelocity > minVToThrow)
            {
                Vector3 throwDirection = ((throwTarget.position - transform.position).normalized + Quaternion.Euler(45, 0, 0) * (new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0))).normalized;
                GetComponent<Rigidbody>().AddForce(throwDirection * throwVelocity);
                GetComponent<Rigidbody>().useGravity = true;
                grabbable = false;
                //OnThrow.Invoke();
            }
            else
            {
                transform.position = startingPosition;
                holding = false;
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
