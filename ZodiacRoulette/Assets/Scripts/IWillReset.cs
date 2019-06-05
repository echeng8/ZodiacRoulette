using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the GameManager to call the scripts resetGameObject() method when the game restarts.
/// last edited - ec 
/// </summary>
public interface IWillReset 
{
    void resetGameObject();
}
