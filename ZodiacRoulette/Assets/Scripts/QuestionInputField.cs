using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class QuestionInputField : MonoBehaviour, IWillReset
{
    InputField inputField;
    CanvasGroup canvasGroup;
    private void Awake()
    {
        inputField = GetComponent<InputField>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void resetGameObject()
    {
        inputField.text = "";
        inputField.interactable = true;
        canvasGroup.alpha = 1;        
    }

}
