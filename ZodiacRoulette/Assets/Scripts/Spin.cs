using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spins the GameObject along the y-axis 
/// -last edited: ec
/// </summary>
public class Spin : MonoBehaviour
{
    [SerializeField] float spinSpeed, secToStop;

    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;
    float speed, speedDecayRate;
    bool stopped = false;

    void Start()
    {

        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponentInChildren<Rigidbody>();

        //calculates the speedDecayRate based on secToStop
        speedDecayRate = spinSpeed / secToStop;
    }

    void FixedUpdate()
    {
        if (stopped)
        {
            if (spinSpeed > 0)
                spinSpeed -= speedDecayRate * Time.deltaTime;
            else
                spinSpeed = 0; 
        }

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, spinSpeed, 0) * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

    }
    public void stopSpinning()
    {
        stopped = true; 
    }
}
