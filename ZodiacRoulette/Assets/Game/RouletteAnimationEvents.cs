using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteAnimationEvents : MonoBehaviour
{
    [SerializeField] GameObject ritualDisplay;
    public void StartSpin()
    {
        Spin spin = GetComponent<Spin>();
        spin.enabled = true;
        ritualDisplay.SetActive(true);
    }

    public void RemoveDisplay()
    {
        ritualDisplay.SetActive(false);
        GameManager.Instance.canThrow = true;
        GameObject.FindGameObjectWithTag("Ball").GetComponent<Throwable>().grabbable = true;
    }
}
