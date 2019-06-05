using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class QuestionInputField : MonoBehaviour, IWillReset
{
    InputField inputField;
    private void Awake()
    {
        inputField = GetComponent<InputField>(); 
    }
    public void resetGameObject()
    {
        inputField.text = "";
        inputField.interactable = true; 
    }
}
