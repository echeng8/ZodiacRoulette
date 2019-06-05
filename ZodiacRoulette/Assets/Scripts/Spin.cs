using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spins the GameObject along the y-axis 
/// -last edited: ec
/// </summary>
public class Spin : MonoBehaviour, IWillReset
{
    [SerializeField] float spinSpeed, secToStop;

    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    float speed, speedDecayRate;
    bool stopped = false;

    void Start()
    {

        
        m_Rigidbody = GetComponentInChildren<Rigidbody>();//Fetch the Rigidbody from the GameObject with this script attached


        speed = spinSpeed; 
        speedDecayRate = spinSpeed / secToStop;//calculates the speedDecayRate based on secToStop
    }

    void FixedUpdate()
    {
        if (stopped)
        {
            if (speed > 0)
                speed -= speedDecayRate * Time.deltaTime;
            else
                speed = 0; 
        }

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, speed, 0) * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

    }
    public void stopSpinning()
    {
        stopped = true; 
    }

    public void resetGameObject()
    {
        stopped = false;
        speed = spinSpeed; 
    }
}
