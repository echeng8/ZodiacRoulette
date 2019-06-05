using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteAnimationEvents : MonoBehaviour
{
    public void StartSpin()
    {
        Spin spin = GetComponent<Spin>();
        spin.enabled = true;
    }
}
