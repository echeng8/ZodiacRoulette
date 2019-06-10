using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// displays fortunes from gamemanager fortuneText dictionary
/// last edited: ec
/// </summary>
public class FortuneDisplay : MonoBehaviour
{
    [SerializeField] Text sign, textDisplay;

    private void Start()
    {
        Sign activeSign = GameManager.Instance.activatedSign; 
        textDisplay.text = GameManager.Instance.fortuneText[activeSign.ToString()];
        sign.text = activeSign.ToString(); 
        
    }
}
