using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Throwable>().resetGameObject();
        }
    }
}
